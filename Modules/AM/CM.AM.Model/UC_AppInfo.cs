using System;

namespace CM.AM.Model
{
    [Serializable]
    public class UC_AppInfo
    {
        public string Id { get; set; }
        public string AppName { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public string AddressURL { get; set; }
        public int? Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
