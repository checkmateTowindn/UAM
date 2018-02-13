using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class Login:BaseModel
    {
        public string LoginName { get; set; }
        public string PassWord { get; set; }
    }
}
