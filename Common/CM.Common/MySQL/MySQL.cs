using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Reflection;
using CM.Common.Interface;
using CM.Common.MySQL;

namespace CM.Common.Data
{
    public class MySQL : IDataBase
    {
        internal MySQL() { }

        public static int CommandTimeOut = 600;
        
        public static string ConnectionString = @"Host=slightcold.date;Port=3306;Username=root;Password=qaz123456;Database=UAM";

        public DataSet ExecuteDataSet(string sql, CommandType cmdType=CommandType.Text, Dictionary<string, object> dic = null)
        {
            DataSet result = null;

            using (MySqlConnection connection = MySQLConnection.GetMySqlConnection())
            {
                try
                {
                    MySqlCommand command = new MySqlCommand();
                    List<MySqlParameter> parameters = new List<MySqlParameter>();
                    if (dic != null)
                    {
                        foreach (var item in dic)
                        {
                            object type = item.Value;
                            type = type ?? DBNull.Value;
                            MySqlParameter parm = new MySqlParameter(item.Key, type);
                            parameters.Add(parm);
                        }
                    }
                    command = MySQLConnection.GetMySqlCommand(connection, sql, parameters.ToArray(), CommandType.Text);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;
                    result = new DataSet();
                    adapter.Fill(result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (connection != null && connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }

            return result;
        }
        
        public IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效  
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;

            DataTable dt = dataSet.Tables[tableIndex];

            IList<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象  
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性  
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值  
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list.ToList();
        }

        public int ExecuteNonQuery(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text)
        {
            MySqlConnection connection = MySQLConnection.GetMySqlConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    object type = item.Value;
                    type = type ?? DBNull.Value;
                    MySqlParameter parm = new MySqlParameter(item.Key, type);
                    parameters.Add(parm);
                }
            }
            int value = -1;
            var command = MySQLConnection.GetMySqlCommand(connection, sql, parameters.ToArray(), CommandType.Text);
            value = command.ExecuteNonQuery();

            command.Dispose();
            MySQLConnection.ReturnConnection(connection);

            return value;

        }

        public IList<T> ExecuteReader<T>(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new()
        {
            List<T> list = null;

            var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    object type = item.Value;
                    type = type ?? DBNull.Value;
                    MySqlParameter parm = new MySqlParameter(item.Key, type);
                    parameters.Add(parm);
                }
            }
            MySqlConnection connection = MySQLConnection.GetMySqlConnection();
            var commond = MySQLConnection.GetMySqlCommand(connection, sql, parameters.ToArray(), commandType);

     

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

            reader.Close();
            commond.Dispose();
            MySQLConnection.ReturnConnection(connection);

            return list;
        }

        public IList<T> ExecuteReader<T>(String sql, bool isType, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new()
        {
            List<T> list = null;

            var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    object type = item.Value;
                    type = type ?? DBNull.Value;
                    MySqlParameter parm = new MySqlParameter(item.Key, type);
                    parameters.Add(parm);
                }
            }
            MySqlConnection connection = MySQLConnection.GetMySqlConnection();
            var commond = MySQLConnection.GetMySqlCommand(connection, sql, parameters.ToArray(), commandType);

            Type ts = Activator.CreateInstance<T>().GetType();
            var obj = ts.GetProperties();

            var reader = commond.ExecuteReader();
            if (reader.Read())
            {
                Dictionary<string, int> Maps = null;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    foreach (var item in obj)
                    {
                        if (item.Name.Equals(reader.GetName(i)))//名称相同
                        {
                            if (Maps == null) { Maps = new Dictionary<string, int>(); }
                            Maps.Add(item.Name, i);
                        }
                    }
                }
                if (Maps != null)
                {
                    list = new List<T>();
                    do
                    {
                        T t = new T();
                        for (int i = 0; i < Maps.Count(); i++)
                        {
                            var val = reader.GetValue(i) == DBNull.Value ? null : reader.GetValue(i);
                            obj[i].SetValue((object)t, val, null);
                        }
                        list.Add(t);
                    } while (reader.Read());
                }
            }

            reader.Close();
            commond.Dispose();
            MySQLConnection.ReturnConnection(connection);

            return list;
        }

        public string ExecuteSingle(string sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text)
        {
            MySqlConnection connection = MySQLConnection.GetMySqlConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    object type = item.Value;
                    type = type ?? DBNull.Value;
                    MySqlParameter parm = new MySqlParameter(item.Key, type);
                    parameters.Add(parm);
                }
            }
            var commond = MySQLConnection.GetMySqlCommand(connection, sql, parameters.ToArray(), commandType);

            var value = commond.ExecuteScalar().ToString();

            commond.Dispose();
            MySQLConnection.ReturnConnection(connection);

            return value;
        }
        
        public IList<T> Get<T>(string commandText, Dictionary<string, object> parameterValues = null, CommandType cmdtype = CommandType.Text) where T : class
        {
            IList<T> list = new List<T>();
            DataSet dataSet = ExecuteDataSet(commandText, cmdtype, parameterValues);
            if (dataSet != null && dataSet.Tables.Count > 0)
                list = DataSetToList<T>(dataSet,0);
            return list;
        }

        public IList<T> Paging<T>(List<string> fields, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string sublistSql, ref int p_RecordCount, Dictionary<string, object> dic = null) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static IDataBase GetDataBase()
        {
            return new MySQL();
        }
    }
}