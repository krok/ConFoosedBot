using System;
using System.Threading.Tasks;
using Confoosed.MatchLogic;
using Confoosed.MatchLogic.Model;
using Confoosed.MatchLogic.Parsing;
using Microsoft.Bot.Connector;

namespace ConFoosedBot.Ranking.CommandHandlers
{
    public class RegisterMatchHandler
    {
        public static bool Handles(Activity activity)
        {
            return MatchParser.TryParse(activity.Text, out Match match);
        }

        public static async Task HandleAsync(Activity activity)
        {
            MatchParser.TryParse(activity.Text, out Match match);
            MatchRegistry.Add(activity.ChannelId, match);
            var reply = activity.CreateReply("The match was registered");
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            await connector.Conversations.ReplyToActivityAsync(reply);
        }
    }
}