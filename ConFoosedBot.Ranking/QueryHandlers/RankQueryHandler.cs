using System;
using System.Linq;
using System.Threading.Tasks;
using Confoosed.MatchLogic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ConFoosedBot.Ranking.QueryHandlers
{
    public class RankQueryHandler
    {
        public static bool Match(IMessageActivity activity)
        {
            return activity.Text.Equals("Rank", StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task StartAsync(IDialogContext context)
        {
            var ranking = LadderRanking.GetPlayersByRanking(MatchRegistry.GetMatches(context.Activity.ChannelId));
            await context.PostAsync("The current ranking is: " + string.Join(", ", ranking.Select(p => p.Id)));
        }
    }
}