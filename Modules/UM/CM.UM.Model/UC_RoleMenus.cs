using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_RoleMenus
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string MenuId { get; set; }
    }
}
