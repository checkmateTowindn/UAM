using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_Roles
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public int RoleType { get; set; }
        public string Description { get; set; }
        public string CreateUser { get; set; }
        public DateTime Createtime { get; set; }
    }
}
