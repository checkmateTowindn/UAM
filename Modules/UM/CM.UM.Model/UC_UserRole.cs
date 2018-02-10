using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_UserRole
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
