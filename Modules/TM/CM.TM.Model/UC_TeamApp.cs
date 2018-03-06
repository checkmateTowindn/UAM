using System;
using System.Collections.Generic;
using System.Text;

namespace CM.TM.Model
{
    [Serializable]
    public class UC_TeamApp
    {
        private string id;
        private string teamid;
        private string appid;
        private DateTime createtime;
        private string createuser;
        public UC_TeamApp(){
        }
        public UC_TeamApp(string id, string teamid, string appid, DateTime createtime, string createuser)
        {
            this.Id = id;
            this.TeamId = teamid;
            this.AppId = appid;
            this.CreateTime = createtime;
            this.CreateUser = createuser;
        }

        public string Id { get => id; set => id = value; }
        public string TeamId { get => teamid; set => teamid = value; }
        public string AppId { get => appid; set => appid = value; }
        public DateTime CreateTime { get => createtime; set => createtime = value; }
        public string CreateUser { get => createuser; set => createuser = value; }
    }
}
