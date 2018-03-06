using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_UserExtend:UC_User
    {
        private string qq;
        private string openid;
        private string realname;
        private string address;
        private DateTime updatetime;
        public UC_UserExtend() { }
        public UC_UserExtend( string qq, string openid, string realname, string address, DateTime updatetime)
        {
            this.QQ = qq;
            this.OpenId = openid;
            this.RealName = realname;
            this.Address = address;
            this.UpdateTime = updatetime;
        }
        public string QQ { get => qq; set => qq = value; }
        public string OpenId { get => openid; set => openid = value; }
        public string RealName { get => realname; set => realname = value; }
        public string Address { get => address; set => address = value; }
        public DateTime UpdateTime { get => updatetime; set => updatetime = value; }
    }
}
