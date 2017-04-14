using System;

namespace Confoosed.MatchLogic.Model
{
    public class FoosMatch
    {
        private static int _idCounter = 1;

        public FoosMatch(Player winner, Player looser)
            : this(winner, looser, null, null, DateTime.UtcNow)
        {
        }

        public FoosMatch(Player winner, Player looser, int? goalsWinner, int? goalsLooser)
            : this(winner, looser, goalsWinner, goalsLooser, DateTime.UtcNow)
        {
        }

        public FoosMatch(Player winner, Player looser, int? goalsWinner, int? goalsLooser, DateTime registered)
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

        public DateTime Registered { get; }
        public Player Winner { get; }
        public Player Looser { get; }

        public int? GoalsWinner { get; }
        public int? GoalsLooser { get; }
    }
}