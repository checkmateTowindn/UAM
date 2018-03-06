using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_LoginLog
    {
        private string id;
        private string appid;
        private string userid;
        private string logintime;
        private string loginip;
        public UC_LoginLog() { }
        public UC_LoginLog(string id, string appid, string userid, string logintime, string loginip)
        {
            this.id = id;
            this.appid = appid;
            this.userid = userid;
            this.logintime = logintime;
            this.loginip = loginip;
        }

        public string Id { get => id; set => id = value; }
        public string AppId { get => appid; set => appid = value; }
        public string UserId { get => userid; set => userid = value; }
        public string LoginTime { get => logintime; set => logintime = value; }
        public string LoginIP { get => loginip; set => loginip = value; }
    }
}
