using System;

namespace Confoosed.MatchLogic.Model
{
    [Serializable]
    public class Match
    {
        private static int _idCounter = 1;

        public Match()
        {

        }

        public Match(Player winner, Player looser)
            : this(winner, looser, null, null, DateTime.UtcNow)
        {
        }

        public Match(Player winner, Player looser, int? goalsWinner, int? goalsLooser)
            : this(winner, looser, goalsWinner, goalsLooser, DateTime.UtcNow)
        {
        }

        public Match(Player winner, Player looser, int? goalsWinner, int? goalsLooser, DateTime registered)
        {
            Winner = winner;
            Looser = looser;
            GoalsWinner = goalsWinner;
            GoalsLooser = goalsLooser;
            Registered = registered;
            Id = _idCounter++;
            winner.AddMatch(this);
            looser.AddMatch(this);
        }

        public int Id { get; set; }

        public DateTime Registered { get; set; }
        public Player Winner { get; set; }
        public Player Looser { get; set; }

        public int? GoalsWinner { get; set; }
        public int? GoalsLooser { get; set; }

        public override string ToString()
        {
            return $"{Winner} trashed {Looser}";
        }
    }
}