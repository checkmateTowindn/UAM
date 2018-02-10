using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_Menus
    {
        public string Id { get; set; }
        public string MenuName { get; set; }
        public string AppId { get; set; }
        public string URL { get; set; }
        public string ParentId { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
    }
}
