using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_LoginLog
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public string LoginIP { get; set; }
    }
}
