using System;
using System.Collections.Generic;
using System.Linq;

namespace Confoosed.MatchLogic.Model
{
    [Serializable]
    public class Group
    {
        private static int _idCounter = 1;

        public int Id { get; }

        public Group(IEnumerable<Player> players)
        {
            Players = new List<Player>(players);
            Id = _idCounter++;
        }

        public Group() : this(new List<Player>())
        {
        }

        public IList<Player> Players { get; }

        public IEnumerable<Match> GetMatches()
        {
            return Players.SelectMany(p => p.GetMatches());
        }
    }
}