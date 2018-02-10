using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class LoginLog
    {
        public string Id { get; set; }
        public AppInfo AppInfo { get; set; }
        public UserInfo UserInfo { get; set; }
        public DateTime LoginTime { get; set; }
        public string LoginIP { get; set; }
    }
}
