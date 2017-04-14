using Confoosed.MatchLogic.Model;

namespace Confoosed.MatchLogic.Parsing
{
    public static class MatchParser
    {
        public static bool TryParse(string message, out FoosMatch match)
        {
            match = null;
            Player player1;
            Player player2;
            int score1;
            int score2;
            if (!PlayerParser.TryParse(message, out player1, out player2) ||
                !ResultParser.TryParse(message, out score1, out score2)) return false;
            match = new FoosMatch(
                score1 > score2 ? player1 : player2,
                score1 > score2 ? player2 : player1,
                score1 > score2 ? score1 : score2, score1 < score2 ? score1 : score2);
            return true;
        }
    }
}