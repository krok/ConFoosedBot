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
    public class RegisterMatchDialog : IDialog<object>
    {
        public static bool Match(string message)
        {
            return MatchParser.TryParse(message, out Match match);
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (MatchParser.TryParse(message.Text, out Match match))
            {
                MatchRegistry.Add(match);
                await context.PostAsync($"Match registered: {match}");
                context.Done($"Match registered: {match}");
            }
        }
    }
}