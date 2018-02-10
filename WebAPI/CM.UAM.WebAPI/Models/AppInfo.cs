using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.UAM.WebAPI.Models
{
    public class AppInfo
    {
        public string Id { get; set; }
        public string AppName { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public string AddressURL { get; set; }
        public List<Team> Team { get; set; }
        public string Status { get; set; }
    }
}
