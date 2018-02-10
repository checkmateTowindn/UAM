using System;
using System.Collections.Generic;
using System.Text;

namespace CM.AM.Model
{
    [Serializable]
    public class UC_TeamApp
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string AppId { get; set; }
    }
}
