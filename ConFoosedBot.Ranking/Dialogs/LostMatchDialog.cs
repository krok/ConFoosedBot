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
    public class LostMatchDialog : IDialog<object>
    {
        public static bool Match(string message)
        {
            return message.ToLowerInvariant().Contains("lost");
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"To bad {context.Activity.From.Name}, who trashed you?");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (PlayerParser.TryParse(message.Text, out Player winner))
            {
                var match = new Match(winner, new Player($"@{context.Activity.From.Name}"));
                MatchRegistry.Add(context.Activity.ChannelId, match);
                await context.PostAsync($"Match registered: {match}");
                context.Done($"Match registered: {match}");
            }
        }
    }
}
