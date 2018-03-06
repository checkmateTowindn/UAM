using System;
using System.Collections.Generic;
using System.Text;

namespace CM.UM.Model
{
    [Serializable]
    public class UC_OperationLog
    {
        private string id;
        private string userid;
        private string operationtime;
        private string operationtype;
        private string tablename;
        private string dataid;
        private string operationdetails;
        public UC_OperationLog() { }

        public UC_OperationLog(string id, string userid, string operationtime, string operationtype, string tablename, string dataid, string operationdetails)
        {
            this.Id = id;
            this.Userid = userid;
            this.OperationTime = operationtime;
            this.OperationType = operationtype;
            this.TableName = tablename;
            this.DataId = dataid;
            this.OperationDetails = operationdetails;
        }

        public string Id { get => id; set => id = value; }
        public string Userid { get => userid; set => userid = value; }
        public string OperationTime { get => operationtime; set => operationtime = value; }
        public string OperationType { get => operationtype; set => operationtype = value; }
        public string TableName { get => tablename; set => tablename = value; }
        public string DataId { get => dataid; set => dataid = value; }
        public string OperationDetails { get => operationdetails; set => operationdetails = value; }
    }
}
