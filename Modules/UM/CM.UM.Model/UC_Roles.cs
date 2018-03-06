using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_Roles
    {
        private string id;
        private string rolename;
        private int roletype;
        private string description;
        private string createuser;
        private DateTime createtime;
        public UC_Roles() { }
        public UC_Roles(string id, string rolename, int roletype, string description, string createuser, DateTime createtime)
        {
            this.Id = id;
            this.RoleName = rolename;
            this.RoleType = roletype;
            this.Description = description;
            this.CreateUser = createuser;
            this.CreateTime = createtime;
        }

        public string Id { get => id; set => id = value; }
        public string RoleName { get => rolename; set => rolename = value; }
        public int RoleType { get => roletype; set => roletype = value; }
        public string Description { get => description; set => description = value; }
        public string CreateUser { get => createuser; set => createuser = value; }
        public DateTime CreateTime { get => createtime; set => createtime = value; }
    }
}
