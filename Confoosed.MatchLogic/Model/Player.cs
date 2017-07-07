using System;
using System.Collections.Generic;

namespace Confoosed.MatchLogic.Model
{
    [Serializable]
    public class Player
    {
        private readonly IList<Match> _matches;

        public Player()
        {
            _matches = new List<Match>();
        }

        public Player(string id)
        {
            Id = id;
            _matches = new List<Match>();
        }

        public string Id { get; set; }

        public int? Ranking { get; set; }

        public IEnumerable<Match> GetMatches()
        {
            return _matches;
        }

        public void AddMatch(Match match)
        {
            _matches.Add(match);
        }

        public override string ToString()
        {
            return Id;
        }
    }
}