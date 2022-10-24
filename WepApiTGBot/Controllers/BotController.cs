using DynamicExpresso;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramClass;


// ngrok http --host-header=localhost 5056
namespace WepApiTGBot.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotController : ControllerBase
    {
        public ConfigurationBot ConfBot;
        public ApplicationContext DbUser;
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            AccumulatedData accumulatedData = new AccumulatedData(update, ConfBot);
            //    var Users = DbUser.Users.Where(b => b.id == accumulatedData.UserTG.Id);  
            UserSetting user = DbUser.Users.Where(s => s.id == accumulatedData.UserTG.Id).FirstOrDefault<UserSetting>();
            if (user == null)
            {
                user = new UserSetting()
                {
                    id = accumulatedData.UserTG.Id,
                    is_bot = accumulatedData.UserTG.IsBot,
                    language_code = accumulatedData.UserTG.LanguageCode,
                    username = accumulatedData.UserTG.Username,
                    LastMessegeId = 0,
                    LastSendingUpdateProcessing = 0
                };
                try
                {
                    DbUser.Users.Add(user);
                    DbUser.SaveChanges();
                }
                catch (Exception e)
                {

                    throw;
                }

            }

            accumulatedData.UserTGSetting = user;
            accumulatedData.ProcessingIncomingUpdate(update, ConfBot.UpdateProcessings);
            SaveUserSetting(accumulatedData);

            return Ok();
        }

        private void SaveUserSetting(AccumulatedData accumulatedData)
        {
            if (accumulatedData.Processed)
            {
                DbUser.Users.Update(accumulatedData.UserTGSetting);
                DbUser.SaveChanges();
            }
        }

        public BotController(IConfiguration configuration, IOptions<ConfigurationBot> settings, ApplicationContext Db)
        {
            DbUser = Db;
            ConfBot = settings.Value;
        }

    }




}
