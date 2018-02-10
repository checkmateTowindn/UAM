using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class Register
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public string RepeatPassWord { get; set; }
        public string IsValid { get; set; }
        public string Status { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
