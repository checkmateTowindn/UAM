using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CM.UM.Model
{
 
    public class UC_User
    {

        private string id;
        private string username;
        private string loginname;
        private string password;
        private int isvalid;
        private int status;
        private string mobile;
        private string email;
        private string createuser;
        private DateTime createtime;
        public UC_User() { }
        public UC_User(string id, string username, string loginname, string password, int isvalid, int status, string mobile, string email, string createuser, DateTime createtime)
        {
            this.Id = id;
            this.UserName = username;
            this.LoginName = loginname;
            this.PassWord = password;
            this.IsValid = isvalid;
            this.Status = status;
            this.Mobile = mobile;
            this.Email = email;
            this.CreateUser = createuser;
            this.CreateTime = createtime;
        }

        public string Id { get => id; set => id = value; }
        public string UserName { get => username; set => username = value; }
        public string LoginName { get => loginname; set => loginname = value; }
        public string PassWord { get => password; set => password = value; }
        public int IsValid { get => isvalid; set => isvalid = value; }
        public int Status { get => status; set => status = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Email { get => email; set => email = value; }
        public string CreateUser { get => createuser; set => createuser = value; }
        public DateTime CreateTime { get => createtime; set => createtime = value; }


    }
}
