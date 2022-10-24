using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramClass
{
    public class ActionProcessing
    {
        public string Text { get; set; }
        public TypeSend Method { get; set; }
        public string KeyboardText { get; set; }
        public TypeKeyboard TypeKeyboard { get; set; }
    }

}
