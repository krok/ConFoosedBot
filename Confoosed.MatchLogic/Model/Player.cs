using System.Collections.Generic;

namespace Confoosed.MatchLogic.Model
{
    public class Player
    {
        private readonly IList<FoosMatch> _matches;

        public Player(string id)
        {
            Id = id;
            _matches = new List<FoosMatch>();
        }

        public string Id { get; }

        public int? Ranking { get; set; }

        public IEnumerable<FoosMatch> GetMatches()
        {
            return _matches;
        }

        public void AddMatch(FoosMatch match)
        {
            _matches.Add(match);
        }
    }
}