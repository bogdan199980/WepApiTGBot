using DynamicExpresso;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WepApiTGBot.Controllers
{
    public enum TypeReference
    {
        Equals,
        Anymore,
        Smaller,
        Similar
    }

  public enum TypeSend
    {
        sendMessage,
        copyMessage,
        sendPhoto,
        sendAudio,
        sendVideo,
        sendAnimation,
        sendVoice,
        sendMediaGroup,
        sendLocation,
        sendContact
    }

   public struct AccumulatedData 
    {
        public TelegramBotClient Client;
        public string MessageId;
        public ChatId ChartId;
        public Telegram.Bot.Types.Enums.UpdateType TypeUpdate;
        public Update Update;
    }

}
