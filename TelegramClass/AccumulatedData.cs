using System;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using DynamicExpresso;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramClass
{
    public enum TypeKeyboard
    {
        None,
        ReplyKeyboardMarkup,
        InlineKeyboardMarkup
    }
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
        public UserSetting UserTGSetting;
        public User UserTG;
        public ChatId ChartId;
        public UpdateType TypeUpdate;
        public MessageType TypeMessage;
        public Update Update;
        public bool Processed = false;
        public string Text;

        public void SetNewUserTGSetting(Message message, int id_UpdateProcessing)
        {
            UserTGSetting.LastSendingUpdateProcessing = id_UpdateProcessing;
            UserTGSetting.LastMessegeId = message.MessageId;
            UserTGSetting.LastDate = DateTime.Now;
            Processed = true;
        }
        public void ProcessingIncomingUpdate(Update update, UpdateProcessing[] updateProcessings)
        {
            for (int i = 0; i < updateProcessings.Length; i++)
            {
                UpdateProcessing upProces = updateProcessings[i];
                if (GetResultUpdateProcessing(upProces.Reference, upProces.Text))
                {
                    ProcessingСondition[] Conditions = upProces.Conditions;
                    bool ResultCondition = true;

                    for (int j = 0; Conditions != null && j < Conditions.Length; j++)
                    {
                        if (!MakeComparison(Conditions[j]))
                        {
                            ResultCondition = false;
                            break;
                        }
                    }

                    if (ResultCondition)
                    {
                        ActionProcessing[] Actions = upProces.Actions;
                        for (int j = 0; j < Actions.Length; j++)
                        {

                            Message message;
                            Parameter[] parameter = null;
                            
                            if (Actions[j].TypeKeyboard == TypeKeyboard.None)
                            {
                                parameter = new[] {new Parameter("text",
                                              typeof(string),
                                              Actions[j].Text) };
                            }
                            else
                            {
                                IReplyMarkup Keyboard = null;
                                if (Actions[j].TypeKeyboard == TypeKeyboard.ReplyKeyboardMarkup)
                                {
                                    Keyboard = JsonConvert.DeserializeObject<ReplyKeyboardMarkup>(Actions[j].KeyboardText);
                                }
                                else if (Actions[j].TypeKeyboard == TypeKeyboard.InlineKeyboardMarkup)
                                {
                                    Keyboard = JsonConvert.DeserializeObject<InlineKeyboardMarkup>(Actions[j].KeyboardText);
                                }
                                parameter = new[] {new Parameter("text",
                                              typeof(string),
                                              Actions[j].Text),
                                                    new Parameter("replyMarkup",
                                              typeof(IReplyMarkup),
                                              Keyboard) };
                            }


                            message = DispatchMethod(Actions[j].Method, parameter);
                            SetNewUserTGSetting(message, upProces.Id);

                        }
                    }


                }
            }

        }

        public bool GetResultUpdateProcessing(TypeReference Reference, string uPtext)
        {

            if (Reference == TypeReference.Equals)
            {
                return Text == uPtext;
            }
            else if (Reference == TypeReference.Anymore)
            {
                return Text.IndexOf(uPtext) != -1;
            }
            else if (Reference == TypeReference.Similar)
            {
                return int.Parse(Text) > int.Parse(uPtext);
            }
            else if (Reference == TypeReference.Smaller)
            {
                return int.Parse(Text) < int.Parse(uPtext);
            }

            return Text == uPtext;

        }
        public List<Parameter> GetParameters(TypeSend method, Parameter[] parameters)
        {
            if (method == TypeSend.sendMessage)
            {

                List<Parameter> AllParameters = new List<Parameter> {
                        new Parameter("chart_id",typeof(ChatId), ChartId),
                        new Parameter("text",typeof(string), ""),
                        new Parameter("parseMode",typeof(ParseMode), ParseMode.Markdown),
                        new Parameter("entities",typeof( List<MessageEntity>), new List<MessageEntity> {}),
                        new Parameter("disableWebPagePreview",typeof(bool), false),
                        new Parameter("disableNotification",typeof(bool), false),
                        new Parameter("protectContent",typeof(bool), false),
                        new Parameter("replyToMessageId",typeof(int), 0),
                        new Parameter("allowSendingWithoutReply",typeof(bool), false),
                        new Parameter("replyMarkup",typeof(IReplyMarkup), null),
                        new Parameter("cancellationToken",typeof(CancellationToken), null)
            };

                for (int i = 0; i < parameters.Length; i++)
                {
                    for (int j = 0; j < AllParameters.Count; j++)
                    {
                        if (parameters[i].Name == AllParameters[j].Name)
                            AllParameters[j] = parameters[i];
                    }
                }

                return AllParameters;

            }

            return new List<Parameter> { };
        }

        public Message DispatchMethod(TypeSend method, Parameter[] parameter)
        {
            List<Parameter> ParametersForMethod = GetParameters(method, parameter);
            if (method == TypeSend.sendMessage)
            {
                try
                {
                    ChatId chart_id = ParametersForMethod.Find(x => x.Name.Contains("chart_id")).Value as ChatId;
                    string text = ParametersForMethod.Find(x => x.Name.Contains("text")).Value as string;
                    ParseMode parseMode = (ParseMode)ParametersForMethod.Find(x => x.Name.Contains("parseMode")).Value;
                    List<MessageEntity> entities = ParametersForMethod.Find(x => x.Name.Contains("entities")).Value as List<MessageEntity>;
                    bool disableWebPagePreview = (bool)ParametersForMethod.Find(x => x.Name.Contains("disableWebPagePreview")).Value;
                    bool disableNotification = (bool)ParametersForMethod.Find(x => x.Name.Contains("disableNotification")).Value;
                    bool protectContent = (bool)ParametersForMethod.Find(x => x.Name.Contains("protectContent")).Value;
                    int replyToMessageId = (int)ParametersForMethod.Find(x => x.Name.Contains("replyToMessageId")).Value;
                    IReplyMarkup replyMarkup = (IReplyMarkup)ParametersForMethod.Find(x => x.Name.Contains("replyMarkup")).Value;
                    bool allowSendingWithoutReply = (bool)ParametersForMethod.Find(x => x.Name.Contains("allowSendingWithoutReply")).Value;

                    //  IReplyMarkup replyMarkup = ParametersForMethod.Find(x => x.Name.Contains("replyMarkup")).Value as IReplyMarkup;
                    //                

                    Task<Message> result = Client.SendTextMessageAsync(chart_id, text, parseMode, entities, disableWebPagePreview, disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup);
                    return result.Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            return new Message();
        }
        public bool MakeComparison(ProcessingСondition processingСondition)
        {
            Interpreter interpreter = new Interpreter().SetVariable("Update", Update);
            Update.Equals(interpreter);
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
        public AccumulatedData(Update update, ConfigurationBot confBot)
        {

            Client = new TelegramBotClient(confBot.Token);
            Update = update;
            TypeUpdate = update.Type;

            if (TypeUpdate == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                TypeMessage = Update.Message.Type;
                UserTG = Update.Message.From;
                MessageId = Update.Message.MessageId;
                ChartId = Update.Message.Chat.Id;
                Text = Update.Message.Text;
            }

        }
    }
}
