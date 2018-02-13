using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class Register:BaseModel
    {
        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string PassWord2 { get; set; }
        public string VerifyNumber { get; set; }
        public int? IsValid { get; set; }
        public int? Status { get; set; }
    }
}
