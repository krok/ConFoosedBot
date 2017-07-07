using System;
using System.Threading;
using System.Threading.Tasks;
using ConFoosedBot.Ranking.QueryHandlers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ConFoosedBot.Ranking.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("RootDialog.StartAsync");
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (RegisterMatchDialog.Match(message.Text))
                await context.Forward(new RegisterMatchDialog(), ResumeDialog, message, CancellationToken.None);
            else if (StatsQueryHandler.Match(message))
                await new StatsQueryHandler().StartAsync(context);
            else if (LostMatchDialog.Match(message.Text))
                await context.Forward(new LostMatchDialog(), ResumeDialog, message, CancellationToken.None);
            else if (WonMatchDialog.Match(message.Text))
                await context.Forward(new WonMatchDialog(), ResumeDialog, message, CancellationToken.None);
            else if (RankQueryHandler.Match(message))
                await new RankQueryHandler().StartAsync(context);
        }

        private async Task ResumeDialog(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync($"Lost match dialog just told me this: {message}");
            context.Wait(MessageReceivedAsync);
        }
    }
}