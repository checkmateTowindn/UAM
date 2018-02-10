using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class UserInfo:Register
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string OpenId { get; set; }
        public string RealName { get; set; }
        public string AddRess { get; set; }
    }
}
