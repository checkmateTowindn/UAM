using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_RoleMenus
    {
        private string id;
        private string roleid;
        private string menuid;
        public UC_RoleMenus() { }
        public UC_RoleMenus(string id, string roleid, string menuid)
        {
            this.Id = id;
            this.RoleId = roleid;
            this.MenuId = menuid;
        }

        public string Id { get => id; set => id = value; }
        public string RoleId { get => roleid; set => roleid = value; }
        public string MenuId { get => menuid; set => menuid = value; }
    }
}
