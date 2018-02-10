using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public string Description { get; set; }
        public List<UserInfo> Leader { get; set; }
        public List<UserInfo> Leaguer { get; set; }

        public string CreateTime { get; set; }
    }
}
