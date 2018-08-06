using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CryptoBot.Models;
using Telegram.Bot.Types;

namespace CryptoBot.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message  = update.Message; 
            var client   = await Bot.Get();

            

            foreach (var command in commands)
            {
                if (!command.Contains(message.Text)) continue;

                command.Execute(message, client);
                return Ok();
            }
            
            return Ok();
        }

    }
}
