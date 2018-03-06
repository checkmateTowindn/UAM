using System;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_Funs
    {
        private string id;
        private string menuid;
        private string funname;
        private string url;
        private string sort;
        private string description;
        public UC_Funs() { }
        public UC_Funs(string id, string menuid, string funname, string url, string sort, string description)
        {
            this.id = id;
            this.menuid = menuid;
            this.funname = funname;
            this.url = url;
            this.sort = sort;
            this.description = description;
        }

        public string Id { get => id; set => id = value; }
        public string MenuId { get => menuid; set => menuid = value; }
        public string FunName { get => funname; set => funname = value; }
        public string Url { get => url; set => url = value; }
        public string Sort { get => sort; set => sort = value; }
        public string Description { get => description; set => description = value; }
    }
}
