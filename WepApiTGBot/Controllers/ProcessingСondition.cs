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
    public class ProcessingСondition
    {
        public object RightValue;
        public TypeReference Reference;
        public string LeftValue;
        AccumulatedData accumulatedData;

        public bool MakeComparison()
        {
            Interpreter interpreter = new Interpreter().SetVariable("Update", accumulatedData.Update);
            accumulatedData.Update.Equals(interpreter);
            string expression;
            if (Reference == TypeReference.Equals)
            {
                expression = "Update." + LeftValue + ".Equals(RightValue)";
            }
            else
            {
                expression = "Update." + LeftValue + " " + GetOperations() + " RightValue";
            }

            Lambda parsedExpression = interpreter.Parse(expression, new Parameter("RightValue", RightValue));
            return (bool)parsedExpression.Invoke();

        }

        public string GetOperations()
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


    }
}
