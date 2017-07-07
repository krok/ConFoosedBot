using System.Collections.Generic;
using Confoosed.MatchLogic.Model;

namespace Confoosed.MatchLogic
{
    public static class MatchRegistry
    {
        private static readonly List<Match> _matches = new List<Match>();

        public static void Add(Match match)
        {
            _matches.Add(match);
        }

        public static IEnumerable<Match> Matches => _matches;
    }
}
