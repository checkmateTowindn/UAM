using System;

namespace CM.TM.Model
{
    [Serializable]
    public class UC_AppInfo
    {
        private string id;
        private string appname;
        private string token;
        private string description;
        private string addressurl;
        private int status;
        private string createuser;
        private DateTime createtime;
        public UC_AppInfo() { } 
        public UC_AppInfo(string id, string appname, string token, string description, string addressurl, int status, string createuser, DateTime createtime)
        {
            this.Id = id;
            this.AppName = appname;
            this.Token = token;
            this.Description = description;
            this.AddressURL = addressurl;
            this.Status = status;
            this.CreateUser = createuser;
            this.CreateTime = createtime;
        }

        public string Id { get => id; set => id = value; }
        public string AppName { get => appname; set => appname = value; }
        public string Token { get => token; set => token = value; }
        public string Description { get => description; set => description = value; }
        public string AddressURL { get => addressurl; set => addressurl = value; }
        public int Status { get => status; set => status = value; }
        public string CreateUser { get => createuser; set => createuser = value; }
        public DateTime CreateTime { get => createtime; set => createtime = value; }
    }
}
