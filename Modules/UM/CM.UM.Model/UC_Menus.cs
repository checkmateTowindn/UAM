using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_Menus
    {
        private string id;
        private string menuname;
        private string appid;
        private string url;
        private string parentid;
        private string icon;
        private string sort;
        private string level;
        private string description;
        public UC_Menus()
        { }
        public UC_Menus(string id, string menuname, string appid, string url, string parentid, string icon, string sort, string level, string description)
        {
            this.id = id;
            this.menuname = menuname;
            this.appid = appid;
            this.url = url;
            this.parentid = parentid;
            this.icon = icon;
            this.sort = sort;
            this.level = level;
            this.description = description;
        }

        public string Id { get => id; set => id = value; }
        public string MenuName { get => menuname; set => menuname = value; }
        public string AppId { get => appid; set => appid = value; }
        public string Url { get => url; set => url = value; }
        public string ParentId { get => parentid; set => parentid = value; }
        public string Icon { get => icon; set => icon = value; }
        public string Sort { get => sort; set => sort = value; }
        public string Level { get => level; set => level = value; }
        public string Description { get => description; set => description = value; }
    }
}
