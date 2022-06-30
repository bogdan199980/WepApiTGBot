using DynamicExpresso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
// ngrok http --host-header=localhost 5056
namespace WepApiTGBot.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotController : ControllerBase
    {
        private List<UpdateProcessing> UpdateProcessings = new();
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            AccumulatedData data = new AccumulatedData(update);

            TelegramBotClient client = new TelegramBotClient("1745794348:AAE0I_rNE6iDCOAWhqGK17icxJs_zkvBp_Y");
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await client.SendTextMessageAsync(update.Message.From.Id, "answer");
            }
            return Ok();
        }

   //     public BotController(IConfiguration configuration)
   //     {
   //         var config = configuration;
   //     }

        public BotController(IOptions<List<UpdateProcessing>> settings)
        {
            UpdateProcessings = settings.Value;

        }

        public class UpdateProcessing
        {

            public string Text { get; set; }
            public string CallbackQueryData { get; set; }
            public TypeReference Reference { get; set; }
            public ProcessingСondition[] Conditions { get; set; }
            public ActionProcessing[] Actions { get; set; }
            //List<ActionProcessing> Actions = new List<ActionProcessing>();
            // List<ProcessingСondition> Сonditions = new List<ProcessingСondition>() { get; set; }
        }

        public class ActionProcessing
        {
            private string text;
            private TypeSend method;
        }
        public class ProcessingСondition
        {
            public object RightValue { get; set; }
            public TypeReference Reference { get; set; }
            public string LeftValue { get; set; }

        }

    }



     
}
