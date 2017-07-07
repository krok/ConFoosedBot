using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ConFoosedBot.Ranking.CommandHandlers;
using ConFoosedBot.Ranking.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ConFoosedBot.Ranking.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new RootDialog());
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            SystemMessageHandler.HandleSystemMessage(activity);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}