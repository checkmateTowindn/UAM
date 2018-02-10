using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class Login
    {
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string Token { get; set; }
    }
}
