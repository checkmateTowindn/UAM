using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string PassWord { get; set; }
        public int? IsValid { get; set; }
        public int? Status { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string UserExtendId { get; set; }
    }
}
