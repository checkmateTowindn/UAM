using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_OperationLog
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime OperationTime { get; set; }
        public string OperationType { get; set; }
        public string TableName { get; set; }
        public string DataId { get; set; }
        public string OperationDetails { get; set; }
    }
}
