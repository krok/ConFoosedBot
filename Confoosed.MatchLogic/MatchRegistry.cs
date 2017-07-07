using System.Collections.Generic;
using System.Linq;
using Confoosed.MatchLogic.Model;

namespace Confoosed.MatchLogic
{
    public static class MatchRegistry
    {
        private static readonly Dictionary<string,List<Match>> Leagues = new Dictionary<string, List<Match>>();

        public static void Add(string leagueName, Match match)
        {
            if(!Leagues.ContainsKey(leagueName))
                Leagues.Add(leagueName, new List<Match>());
            Leagues[leagueName].Add(match);
        }

        public static IEnumerable<Match> GetMatches(string leagueName)
        {
            return Leagues.ContainsKey(leagueName) ? Leagues[leagueName] : Enumerable.Empty<Match>();
        }
    }
}
