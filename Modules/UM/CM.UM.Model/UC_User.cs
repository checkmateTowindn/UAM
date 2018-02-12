using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CM.UM.Model
{
 
    public class UC_User
    {

        //private string id;
        //private string userName;
        //private string loginName;
        //private string passWord;
        //private int? isValid;
        //private int? status;
        //private string mobile;
        //private string email;
        //private string createUser;
        //private DateTime createTime;
        //private string userExtendId;

        //public string Id { get => id; set => id = value; }
        //public string UserName { get => userName; set => userName = value; }
        //public string LoginName { get => loginName; set => loginName = value; }
        //public string PassWord { get => passWord; set => passWord = value; }
        //public int? IsValid { get => isValid; set => isValid = value; }
        //public int? Status { get => status; set => status = value; }
        //public string Mobile { get => mobile; set => mobile = value; }
        //public string Email { get => email; set => email = value; }
        //public string CreateUser { get => createUser; set => createUser = value; }
        //public DateTime CreateTime { get => createTime; set => createTime = value; }
        //public string UserExtendId { get => userExtendId; set => userExtendId = value; }

       
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
