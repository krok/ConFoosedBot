using System.Text.RegularExpressions;

namespace Confoosed.MatchLogic.Parsing
{
    public static class ResultParser
    {
        private static readonly Regex PlayerRegex = new Regex(@"(\d\d?)", RegexOptions.Compiled);

        public static bool TryParse(string message, out int score1, out int score2)
        {
            score1 = 0;
            score2 = 0;
            var matchCollection = PlayerRegex.Matches(message);
            if (matchCollection.Count != 2)
                return false;

            return int.TryParse(matchCollection[0].Value, out score1) &&
                   int.TryParse(matchCollection[1].Value, out score2);
        }
    }
}