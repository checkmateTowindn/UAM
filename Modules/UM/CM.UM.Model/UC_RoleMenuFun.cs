using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_RoleMenuFun
    {
        private string id;
        private string rolemenuid;
        private string funid;

        public UC_RoleMenuFun(string id, string rolemenuid, string funid)
        {
            this.Id = id;
            this.RoleMenuId = rolemenuid;
            this.FunId = funid;
        }
        public UC_RoleMenuFun() { }
        public string Id { get => id; set => id = value; }
        public string RoleMenuId { get => rolemenuid; set => rolemenuid = value; }
        public string FunId { get => funid; set => funid = value; }
    }
}
