using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DynamicExpresso;

namespace WepApiTGBot.Controllers
{
    public class ActionProcessing
    {
        private string text;
        private TypeSend method;


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
    }
}
