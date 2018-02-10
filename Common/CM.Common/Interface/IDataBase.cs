using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CM.Common.Interface
{
    public interface IDataBase
    {
        int ExecuteNonQuery(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text);

        List<T> ExecuteReader<T>(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new();

        String ExecuteSingle(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text);

        List<T> Paging<T>(List<string> fields, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string sublistSql, ref int p_RecordCount, Dictionary<string, object> dic = null) where T : class, new();
    }
}
