using System;
using System.Linq;
using System.Threading.Tasks;
using Confoosed.MatchLogic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ConFoosedBot.Ranking.QueryHandlers
{
    public class StatsQueryHandler
    {
        public static bool Match(IMessageActivity activity)
        {
            return activity.Text.Equals("Stats", StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task StartAsync(IDialogContext context)
        {
            var mathces = MatchRegistry.Matches;
            await context.PostAsync("Number of matches played: " + mathces.Count());
        }
    }
}