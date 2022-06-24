using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WepApiTGBot.Controllers
{
    public class UpdateProcessing
    {
        private string CallbackQueryData;
        private string Text;
        private TypeReference Reference;

        List<ActionProcessing> Actions = new List<ActionProcessing>();
        List<ProcessingСondition> Сonditions = new List<ProcessingСondition>();
      

    }
}
