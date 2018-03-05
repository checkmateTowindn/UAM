using CM.Common.Interface;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace CM.Common.PostgreSQL
{
    public class PostgreSQL : IDataBase
    {
        public IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(string sql, CommandType cmdType = CommandType.Text, Dictionary<string, object> dic = null)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text)
        {
            NpgsqlConnection connection = PostgreSQLConnection.GetNpgsqlConnection();
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            foreach (var item in dic)
            {
                object type = item.Value;
                type = type ?? DBNull.Value;
                NpgsqlParameter parm = new NpgsqlParameter(item.Key, type);
                parameters.Add(parm);
            }
            int value = -1;
            var command = PostgreSQLConnection.GetNpgsqlCommand(connection, sql, parameters.ToArray(), CommandType.Text);
            value = command.ExecuteNonQuery();

            command.Dispose();
            PostgreSQLConnection.ReturnConnection(connection);

            return value;

        }

        /// <summary>
        /// 将执行SQL后获得的信息映射为类型T的数据，如果数据不为空则返回数据，否则返回null，注意：字段名必须和数据库列名一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> ExecuteReader<T>(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new()
        {
            List<T> list = null;

            var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            foreach (var item in dic)
            {
                object type = item.Value;
                type = type ?? DBNull.Value;
                NpgsqlParameter parm = new NpgsqlParameter(item.Key, type);
                parameters.Add(parm);
            }
            NpgsqlConnection connection = PostgreSQLConnection.GetNpgsqlConnection();
            var commond = PostgreSQLConnection.GetNpgsqlCommand(connection, sql, parameters.ToArray(), commandType);

            var reader = commond.ExecuteReader();
            if (reader.Read())
            {
                //存储对应关系
                Dictionary<FieldInfo, int> Maps = null;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    foreach (var field in fields)
                    {
                        if (field.Name.Equals(reader.GetName(i)))
                        {
                            if (Maps == null) { Maps = new Dictionary<FieldInfo, int>(); }
                            Maps.Add(field, i);
                        }
                    }
                }
                //判断是否有对应关系
                if (Maps != null)
                {
                    list = new List<T>();
                    do
                    {
                        T t = new T();
                        foreach (var map in Maps)
                        {
                            map.Key.SetValue(t, reader.GetValue(map.Value));
                        }
                        list.Add(t);
                    } while (reader.Read());
                }
            }

            reader.CloseAsync();
            commond.Dispose();
            PostgreSQLConnection.ReturnConnection(connection);

            return list;
        }

        public IList<T> ExecuteReader<T>(string sql, bool isType, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 返回执行SQL后，数据库返回的第一个值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">sql所需参数</param>
        /// <param name="commandType">执行sql的类型</param>
        /// <returns></returns>
        public string ExecuteSingle(string sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text)
        {
            NpgsqlConnection connection = PostgreSQLConnection.GetNpgsqlConnection();
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            foreach (var item in dic)
            {
                object type = item.Value;
                type = type ?? DBNull.Value;
                NpgsqlParameter parm = new NpgsqlParameter(item.Key, type);
                parameters.Add(parm);
            }
            var commond = PostgreSQLConnection.GetNpgsqlCommand(connection, sql, parameters.ToArray(), commandType);

            var value = commond.ExecuteScalar().ToString();

            commond.Dispose();
            PostgreSQLConnection.ReturnConnection(connection);

            return value;
        }

        public IList<T> Get<T>(string commandText, Dictionary<string, object> parameterValues = null, CommandType cmdtype = CommandType.Text) where T : class
        {
            throw new NotImplementedException();
        }

        public IDataBase GetDataBase()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// pgsql分页
        /// </summary>
        /// <typeparam name="T">返回的model</typeparam>
        /// <param name="fields">返回的列</param>
        /// <param name="p_PageSize">每页记录数</param>
        /// <param name="p_PageIndex">当前页数</param>
        /// <param name="p_OrderType">排序方式：0-正序 1-倒序</param>
        /// <param name="p_OrderColumnName">排序列名称</param>
        /// <param name="sublistSql">子表sql</param>
        /// <param name="p_RecordCount">总记录数</param>
        /// <returns></returns>
        public List<T> Paging<T>(List<string> fields, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string sublistSql, ref int p_RecordCount, Dictionary<string, object> dic = null) where T : class, new()
        {
            if (p_RecordCount != 0)
            {
                string _SQL = "SELECT COUNT(*) FROM ( " + sublistSql + " )";
                p_RecordCount = ExecuteReader<T>(_SQL).Count;
            }
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT ");
            foreach (string field in fields)
            {
                sql.Append(field);
            }
            sql.Append(@" FROM (" + sublistSql + "");
            sql.Append(@" LIMIT " + p_PageSize + " OFFSET " + p_PageIndex * p_PageSize + " ");
            sql.Append(" ORDER BY " + p_OrderColumnName + " " + (p_OrderType == 1 ? "DESC" : "ASC") + " )");
            List<T> list = ExecuteReader<T>(sql.ToString(), dic);
            return list;
        }

        IList<T> IDataBase.ExecuteReader<T>(string sql, Dictionary<string, object> dic, CommandType commandType)
        {
            throw new NotImplementedException();
        }

        IList<T> IDataBase.Paging<T>(List<string> fields, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string sublistSql, ref int p_RecordCount, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}