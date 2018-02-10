using System;
using System.Collections.Generic;
using System.Text;

namespace CM.AM.Model
{
    [Serializable]
    public class UC_Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Leader { get; set; }
        public DateTime CreateTime { get; set; }
        public string Leaguer { get; set; }
        public string Sign { get; set; }
    }
}
