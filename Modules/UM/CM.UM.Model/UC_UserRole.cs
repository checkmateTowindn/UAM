using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_UserRole
    {
        private string id;
        private string userid;
        private string roleid;

        public UC_UserRole(string id, string userid, string roleid)
        {
            this.Id = id;
            this.UserId = userid;
            this.RoleId = roleid;
        }

        public string Id { get => id; set => id = value; }
        public string UserId { get => userid; set => userid = value; }
        public string RoleId { get => roleid; set => roleid = value; }
    }
}
