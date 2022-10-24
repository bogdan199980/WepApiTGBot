using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramClass
{
    public class UserSetting
    {
        public long db_id { get; set; }
        public long id { get; set; }
        public bool is_bot { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }
        public long LastMessegeId { get; set; }
        public int LastSendingUpdateProcessing { get; set; }
        public DateTime LastDate { get; set; }
    }
}
