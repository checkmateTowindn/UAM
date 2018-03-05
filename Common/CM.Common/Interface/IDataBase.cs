using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CM.Common.Interface
{
    public interface IDataBase
    {
        DataSet ExecuteDataSet(string sql, CommandType cmdType = CommandType.Text, Dictionary<string, object> dic = null);

        IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex);

        IList<T> ExecuteReader<T>(String sql, bool isType, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new();

        IList<T> Get<T>(String commandText, Dictionary<string, object> parameterValues = null, CommandType cmdtype = CommandType.Text) where T : class;     

        int ExecuteNonQuery(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text);

        IList<T> ExecuteReader<T>(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new();

        String ExecuteSingle(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text);

        IList<T> Paging<T>(List<string> fields, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string sublistSql, ref int p_RecordCount, Dictionary<string, object> dic = null) where T : class, new();
    }
}
