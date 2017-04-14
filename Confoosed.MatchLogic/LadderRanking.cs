﻿using System;
using System.Collections.Generic;
using System.Linq;
using Confoosed.MatchLogic.Extensions;
using Confoosed.MatchLogic.Model;

namespace Confoosed.MatchLogic
{
    public static class LadderRanking
    {
        public static IOrderedEnumerable<Player> GetPlayersByRanking(IEnumerable<FoosMatch> matches)
        {
            var list = matches.GetGroupsBySize().ToList();
            if (list.Any(g => g.Players.Any(p => !p.Ranking.HasValue)))
                RankGroupsAndPlayers(list);
            return list.GetGroupsBySize().SelectMany(p => p.Players).OrderBy(p => p.Ranking);
        }

        private static void RankGroupsAndPlayers(IEnumerable<Group> groups)
        {
            var count = 0;
            var list = groups.ToList();
            foreach (var group in list.GetGroupsBySize())
            {
                RankGroupOfPlayers(group, count);
                count = count + group.Players.Count;
            }
        }

        private static void RankGroupOfPlayers(Group group, int count)
        {
            foreach (var match in group.GetMatches().OrderBy(m => m.Id))
                UpdateRanking(group, () => GetNextRank(count, group), match);
        }

        private static int GetNextRank(int count, Group group)
        {
            var lowestRanking = group.Players.Max(p => p.Ranking);
            return lowestRanking > 0 ? lowestRanking.Value + 1 : count + 1;
        }

        private static void UpdateRanking(Group group, Func<int> nextRank, FoosMatch match)
        {
            var winner = match.Winner;
            var looser = match.Looser;
            if (!winner.Ranking.HasValue)
                winner.Ranking = nextRank();

            UpdateRanking(group, winner, looser);

            if (!looser.Ranking.HasValue)
                looser.Ranking = nextRank();
        }

        private static void UpdateRanking(Group group, Player winner, Player looser)
        {
            if (!winner.Ranking.HasValue || !looser.Ranking.HasValue || !(winner.Ranking > looser.Ranking)) return;

            //Check if the winner can challenge the looser
            if (Math.Abs(looser.Ranking.Value - winner.Ranking.Value) > 2)
                return;

            //Check if the looser won the previous to matches n matches
            if (CheckHistory(winner, looser, 2)) return;
            if (CheckHistory(winner, looser, 3)) return;

            SetRanking(group, winner, looser);
        }

        private static void SetRanking(Group group, Player winner, Player looser)
        {
            if (!winner.Ranking.HasValue || !looser.Ranking.HasValue)
                return;

            if (looser.Ranking.Value > winner.Ranking.Value)
                return;

            if (winner.Ranking.Value - looser.Ranking.Value == 2)
            {
                var player =
                    group.Players.FirstOrDefault(p => p.Ranking.GetValueOrDefault() == looser.Ranking.Value + 1);
                if (player != null)
                    player.Ranking = looser.Ranking.Value + 2;
            }
            var newRanking = looser.Ranking;
            looser.Ranking = winner.Ranking;

            winner.Ranking = newRanking;
        }

        private static bool CheckHistory(Player winner, Player looser, int numberOfMatchesToCheck)
        {
            if (winner.GetMatches().Count(m => m.Winner == looser) < numberOfMatchesToCheck) return false;
            {
                if (
                    winner.GetMatches()
                        .Reverse()
                        .Skip(numberOfMatchesToCheck - 1)
                        .Take(numberOfMatchesToCheck)
                        .All(m => m.Winner == looser))
                    return true;
            }
            return false;
        }
    }
}