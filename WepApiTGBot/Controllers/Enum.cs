using DynamicExpresso;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static WepApiTGBot.Controllers.BotController;

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

   public class AccumulatedData 
    {
        public TelegramBotClient Client;
        public int MessageId;
        public ChatId ChartId;
        public Telegram.Bot.Types.Enums.UpdateType TypeUpdate;
        public Update Update;
        public string text;

        public void ProcessingIncomingUpdate(Update update)
        {

        }


        public Parameter[] GetParameters(AccumulatedData accumulatedData, TypeSend method, Parameter[] parameters)
        {
            if (method == TypeSend.sendMessage)
            {

                Parameter[] AllParameters = new[] {
                        new Parameter("chart_id",
                                      typeof(ChatId),
                                      accumulatedData.ChartId),
                        new Parameter("text",typeof(string), ""),
                        new Parameter("parseMode",typeof(ParseMode),null),
                        new Parameter("entities",typeof(MessageEntity), null),
                        new Parameter("disableWebPagePreview",typeof(bool), null),
                        new Parameter("disableNotification",typeof(bool), null),
                        new Parameter("protectContent",typeof(bool), null),
                        new Parameter("replyToMessageId",typeof(int), null),
                        new Parameter("allowSendingWithoutReply",typeof(bool), null),
                        new Parameter("replyMarkup",typeof(IReplyMarkup), null),
                        new Parameter("cancellationToken",typeof(CancellationToken), null)
            };

                for (int i = 0; i < parameters.Length; i++)
                {
                    for (int j = 0; i < AllParameters.Length; i++)
                    {
                        if (parameters[i].Name == AllParameters[j].Name)
                            AllParameters[j] = parameters[i];
                    }
                }

                return AllParameters;

            }

            return new[] {new Parameter("chart_id",
                                      typeof(ChatId),
                                      accumulatedData.ChartId) };
        }

        public Message DispatchMethod(AccumulatedData accumulatedData, TypeSend method, Parameter[] parameter)
        {
            Parameter[] ParametersForMethod = GetParameters(accumulatedData, method, parameter);
            TelegramBotClient Client = accumulatedData.Client;
            if (method == TypeSend.sendMessage)
            {
                Interpreter interpreter = new Interpreter().SetVariable("Client", Client);
                string expression = "Client.SendTextMessageAsync(chart_id, text, parseMode, entities, disableWebPagePreview, disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);";
                Lambda parsedExpression = interpreter.Parse(expression, ParametersForMethod);
                Message result = (Message)parsedExpression.Invoke();
                return result;
            }

            return new Message();
        }
        public bool MakeComparison(ProcessingСondition processingСondition, AccumulatedData accumulatedData)
        {
            Interpreter interpreter = new Interpreter().SetVariable("Update", accumulatedData.Update);
            accumulatedData.Update.Equals(interpreter);
            string expression;
            if (processingСondition.Reference == TypeReference.Equals)
            {
                expression = "Update." + processingСondition.LeftValue + ".Equals(RightValue)";
            }
            else
            {
                expression = "Update." + processingСondition.LeftValue + " " + GetOperations(processingСondition.Reference) + " RightValue";
            }

            Lambda parsedExpression = interpreter.Parse(expression, new Parameter("RightValue", processingСondition.RightValue));
            return (bool)parsedExpression.Invoke();

        }
        public string GetOperations(TypeReference Reference)
        {

            if (Reference == TypeReference.Equals)
            {
                return "==";
            }
            else if (Reference == TypeReference.Anymore)
            {
                return "==";
            }
            else if (Reference == TypeReference.Similar)
            {
                return ">";
            }
            else if (Reference == TypeReference.Smaller)
            {
                return "<";
            }

            return "==";

        }
        public AccumulatedData(Update update)
        {

            Client = new TelegramBotClient("1745794348:AAE0I_rNE6iDCOAWhqGK17icxJs_zkvBp_Y");
            Update = update;
            TypeUpdate = update.Type;
         
            if (Update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                MessageId = Update.Message.MessageId;
                ChartId = Update.Message.Chat.Id;
                text = Update.Message.Text;
            }

        }
    }

}
