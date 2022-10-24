using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.Enums;

namespace TelegramClass
{
    public class UpdateProcessing
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public TypeReference Reference { get; set; }
        public MessageType MesType { get; set; }
        public UpdateType UpType { get; set; }
        public ProcessingСondition[] Conditions { get; set; }
        public ActionProcessing[] Actions { get; set; }
    }
}
