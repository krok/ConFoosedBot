using System.Text.RegularExpressions;
using Confoosed.MatchLogic.Model;

namespace Confoosed.MatchLogic.Parsing
{
    public static class PlayerParser
    {
        private static readonly Regex PlayerRegex = new Regex("(@[a-zA-Z]+)", RegexOptions.Compiled);

        public static bool TryParse(string message, out Player player1, out Player player2)
        {
            player1 = null;
            player2 = null;

            var matchCollection = PlayerRegex.Matches(message);
            if (matchCollection.Count != 2)
                return false;
            player1 = new Player(matchCollection[0].Value);
            player2 = new Player(matchCollection[1].Value);
            return true;
        }
    }
}