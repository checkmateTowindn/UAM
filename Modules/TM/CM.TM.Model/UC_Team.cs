using System;
using System.Collections.Generic;
using System.Text;

namespace CM.TM.Model
{
    [Serializable]
    public class UC_Team
    {
        private string id;
        private string name;
        private string description;
        private string leader;
        private DateTime createtime;
        private string createuser;
        private string sign;
        private string logo;
        private int projectcount;
        private int status;

        public UC_Team()
        {

        }
        public UC_Team(string id, string name, string description, string leader, DateTime createtime, string createuser, string sign, string logo,int projectcount, int status)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Leader = leader;
            this.CreateTime = createtime;
            this.CreateUser = createuser;
            this.Sign = sign;
            this.Logo = logo;
            this.ProjectCount = projectcount;
            this.Status = status;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Leader { get => leader; set => leader = value; }
        public DateTime CreateTime { get => createtime; set => createtime = value; }
        public string CreateUser { get => createuser; set => createuser = value; }
        public string Sign { get => sign; set => sign = value; }
        public string Logo { get => logo; set => logo = value; }
        public int ProjectCount { get => projectcount; set => projectcount = value; }
        /// <summary>
        /// 0：已删除，1：正常，2：已解散
        /// </summary>
        public int Status { get => status; set => status = value; }
    }
}
