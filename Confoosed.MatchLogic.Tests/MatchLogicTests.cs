using System;
using System.Collections.Generic;
using System.Linq;
using Confoosed.MatchLogic.Extensions;
using Confoosed.MatchLogic.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Confoosed.MatchLogic.Tests
{
    [TestClass]
    public class MatchLogicTests
    {
        [TestMethod]
        public void GetPlayers()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player3),
                    new Match(player1, player2),
                    new Match(player3, player2),
                };

            var actual = matches.GetPlayers().ToArray();
            Assert.AreEqual(3, actual.Length);
            Assert.IsTrue(actual.Any(p => p == player1));
            Assert.IsTrue(actual.Any(p => p == player2));
            Assert.IsTrue(actual.Any(p => p == player3));
        }

        [TestMethod]
        public void GetGroups()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player3),
                    new Match(player1, player2),
                    new Match(player3, player2),
                    new Match(player4, player5),
                    new Match(player5, player6),
                    new Match(player6, player7),
                    new Match(player8, player9),
                };

            Assert.AreEqual(3, matches.GetGroups().Count());
        }

        [TestMethod]
        public void GroupsShouldBeOrderedBySize()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player3),
                    new Match(player1, player2),
                    new Match(player3, player2),
                    new Match(player4, player5),
                    new Match(player5, player6),
                    new Match(player6, player7),
                    new Match(player8, player9),
                };

            var actual = matches.GetGroups().GetGroupsBySize();
            Assert.AreEqual(4, actual.First().Players.Count);
            Assert.AreEqual(3, actual.Skip(1).First().Players.Count);
            Assert.AreEqual(2, actual.Skip(2).First().Players.Count);

            CollectionAssert.AreEqual(new[] {"4", "5", "6", "7"}, actual.First().Players.Select(p => p.Id).ToArray());
            CollectionAssert.AreEqual(new[] {"1", "2", "3"}, actual.Skip(1).First().Players.Select(p => p.Id).ToArray());
            CollectionAssert.AreEqual(new[] {"8", "9"}, actual.Skip(2).First().Players.Select(p => p.Id).ToArray());
        }

        [TestMethod]
        public void PlayersShouldBeRankedByResults()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");


            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player3),
                    new Match(player1, player2),
                    new Match(player3, player2),
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            CollectionAssert.AreEqual(new[] {"1", "3", "2"}, actual.ToArray(), string.Join(",", actual));
        }

        [TestMethod]
        public void WhenRankingTwoGamesBeatsOne()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player2, player1),
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            CollectionAssert.AreEqual(new[] {"1", "2"}, actual.ToArray(), string.Join(",", actual));
        }

        [TestMethod]
        public void WhenRankingTwoGamesBeatsTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player2, player1),
                    new Match(player2, player1),
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            CollectionAssert.AreEqual(new[] {"2", "1"}, actual.ToArray(), string.Join(",", actual));
        }

        [TestMethod]
        public void WhenRankingThreeGamesBeatTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player2, player1),
                    new Match(player2, player1),
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            CollectionAssert.AreEqual(new[] {"1", "2"}, actual.ToArray(), string.Join(",", actual));
        }

        [TestMethod]
        public void WhenRankingThreeGamesBeatThree()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player2, player1),
                    new Match(player2, player1),
                    new Match(player2, player1),
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            CollectionAssert.AreEqual(new[] {"2", "1"}, actual.ToArray(), string.Join(",", actual));
        }

        [TestMethod]
        public void PlayersShouldBeRankedByGroupSizeAndResults()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player3),
                    new Match(player1, player2),
                    new Match(player3, player2),
                    new Match(player4, player5),
                    new Match(player5, player6),
                    new Match(player6, player7),
                    new Match(player8, player9),
                };


            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            CollectionAssert.AreEqual(new[] {"4", "5", "6", "7", "1", "3", "2", "8", "9"}, actual.ToArray(),
                string.Join(",", actual));
        }

        [TestMethod]
        public void GroupsShouldBeOrderedByLastMatchPlayed()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player1, player2),
                    new Match(player4, player5),
                    new Match(player5, player6),
                    new Match(player6, player7),
                    new Match(player8, player9, null, null, DateTime.Now.AddDays(1)),
                };


            var actual = matches.GetGroupsBySize();
            Assert.AreEqual(4, actual.First().Players.Count);
            Assert.AreEqual(2, actual.Skip(1).First().Players.Count);
            Assert.AreEqual(2, actual.Skip(2).First().Players.Count);

            CollectionAssert.AreEqual(new[] {"4", "5", "6", "7"}, actual.First().Players.Select(p => p.Id).ToArray());
            CollectionAssert.AreEqual(new[] {"8", "9"}, actual.Skip(1).First().Players.Select(p => p.Id).ToArray());
            CollectionAssert.AreEqual(new[] {"1", "2"}, actual.Skip(2).First().Players.Select(p => p.Id).ToArray());
        }

        [TestMethod]
        public void PerfTest()
        {
            var players = new List<Player>();
            for (var i = 0; i < 30; i++)
                players.Add(new Player(i.ToString()));

            var matches = new List<Match>();

            for (var i = 0; i < 28; i++)
                matches.Add(new Match(players[i], players[i + 1]));
            matches.Add(new Match(players[0], players[29]));

            var random = new Random();
            for (var i = 0; i < 2000; i++)
                matches.Add(new Match(players[random.Next(0, 15)], players[new Random().Next(16, 29)]));

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();
            Assert.AreEqual(30, actual.Count);
        }

        [TestMethod]
        public void MatchesOnlyAffectRankingWhenLooserIsRankedBetterThanWinnerByMax2()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var player4 = new Player("4");
            var player5 = new Player("5");

            //Ranking 1,2,3,4,5
            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player2, player3),
                    new Match(player3, player4),
                    new Match(player4, player5),
                    new Match(player5, player1) //This match has no effect
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            CollectionAssert.AreEqual(new[] {"1", "2", "3", "4", "5"}, actual.ToArray(), string.Join(",", actual));
        }


        [TestMethod]
        public void LooserShouldBeMoveDownByOne()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var player4 = new Player("4");
            var player5 = new Player("5");

            //Ranking 1,2,3,4,5
            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player2, player3),
                    new Match(player3, player4),
                    new Match(player4, player5),
                    new Match(player5, player1), //This match has no effect
                    new Match(player3, player1)
                };

            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            CollectionAssert.AreEqual(new[] {"3", "1", "2", "4", "5"}, actual.ToArray(), string.Join(",", actual));
        }

        [TestMethod]
        public void WhenPlayersFromDifferentGroupsPlay()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");

            var player4 = new Player("4");
            var player5 = new Player("5");
            var player6 = new Player("6");
            var player7 = new Player("7");

            var player8 = new Player("8");
            var player9 = new Player("9");

            var matches =
                new[]
                {
                    new Match(player1, player2),
                    new Match(player2, player3),
                    new Match(player4, player5),
                    new Match(player5, player6),
                    new Match(player6, player7),
                    new Match(player8, player9),
                    new Match(player4, player2),
                    new Match(player8, player6),
                };


            var actual = LadderRanking.GetPlayersByRanking(matches).Select(p => p.Id).ToList();

            CollectionAssert.AreEqual(new[] {"1", "4", "2", "3", "5", "8", "6", "7", "9"}, actual.ToArray(),
                string.Join(",", actual));
        }
    }
}