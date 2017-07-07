using System;
using System.Threading.Tasks;
using Confoosed.MatchLogic;
using Confoosed.MatchLogic.Model;
using Confoosed.MatchLogic.Parsing;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ConFoosedBot.Ranking.Dialogs
{
    [Serializable]
    public class WonMatchDialog : IDialog<object>
    {
        public static bool Match(string message)
        {
            return message.ToLowerInvariant().Contains("won");
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"Good for you {context.Activity.From.Name}, who did you trash?");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (PlayerParser.TryParse(message.Text, out Player looser))
            {
                var match = new Match(new Player($"@{context.Activity.From.Name}"), looser);
                MatchRegistry.Add(context.Activity.ChannelId, match);
                await context.PostAsync($"Match registered: {match}");
                context.Done($"Match registered: {match}");
            }
        }
    }
}
