using Confoosed.MatchLogic.Model;

namespace Confoosed.MatchLogic.Parsing
{
    public static class MatchParser
    {
        public static bool TryParse(string message, out Match match)
        {
            match = null;
            var tryParsePlayers = PlayerParser.TryParse(message, out Player player1, out Player player2);
            if (tryParsePlayers && message.ToLowerInvariant().Contains(" lost "))
            {
                match = new Match(player2, player1);
                return true;
            }

            if (tryParsePlayers && message.ToLowerInvariant().Contains(" won "))
            {
                match = new Match(player1, player2);
                return true;
            }

            if (!tryParsePlayers ||
                !ResultParser.TryParse(message, out int score1, out int score2))
                return false;
            match = new Match(
                score1 > score2 ? player1 : player2,
                score1 > score2 ? player2 : player1,
                score1 > score2 ? score1 : score2, score1 < score2 ? score1 : score2);
            return true;
        }
    }
}