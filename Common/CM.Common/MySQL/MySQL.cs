using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.Generic;
using CM.Common.Model;
using System.Reflection;

namespace CM.Common.MySQL
{
    /// <summary>
    /// MySqlHelper操作类
    /// </summary>
    public static class MySqlHelper
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        public static int CommandTimeOut = 600;

        /// <summary>
        ///初始化MySqlHelper实例
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public static string ConnectionString = @"Host=slightcold.date;Port=3306;Username=root;Password=qaz123456;Database=UAM";

        /// <summary>  
        /// 返回DataSet  
        /// </summary>  
        /// <param name="cmdText">命令字符串</param>  
        /// <param name="cmdType">命令类型</param>  
        /// <param name="commandParameters">可变参数</param>  
        /// <returns> DataSet </returns>  
        public static DataSet ExecuteDataSet(string sql, CommandType cmdType=CommandType.Text, Dictionary<string, object> dic = null)
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
        /// <summary>  
        /// DataSetToList  
        /// </summary>  
        /// <typeparam name="T">转换类型</typeparam>  
        /// <param name="dataSet">数据源</param>  
        /// <param name="tableIndex">需要转换表的索引</param>  
        /// <returns></returns>  
        public static List<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
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


        public static int ExecuteNonQuery(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text)
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

        /// <summary>
        /// 将执行SQL后获得的信息映射为类型T的数据，如果数据不为空则返回数据，否则返回null，注意：字段名必须和数据库列名一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> ExecuteReader<T>(String sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new()
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




        public static List<T> ExecuteReader<T>(String sql, bool isType, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text) where T : class, new()
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


        /// <summary>
        /// 返回执行SQL后，数据库返回的第一个值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">sql所需参数</param>
        /// <param name="commandType">执行sql的类型</param>
        /// <returns></returns>
        public static string ExecuteSingle(string sql, Dictionary<string, object> dic = null, CommandType commandType = CommandType.Text)
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

        /// <summary>
        /// 执行查询
        /// 
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam><param name="commandText">查询语句或储存过程名</param><param name="parameterValues">参数</param><param name="cmdtype">文本或储存过程</param><param name="timeout">超时时间，单位秒，为null不重新设置</param>
        /// <returns/>
        public static IList<T> Get<T>(string commandText, Dictionary<string, object> parameterValues = null, CommandType cmdtype = CommandType.Text) where T : class
        {
            IList<T> list = (IList<T>)new List<T>();
            DataSet dataSet = ExecuteDataSet(commandText, cmdtype, parameterValues);
            if (dataSet != null && dataSet.Tables.Count > 0)
                list = DataSetToList<T>(dataSet,0);
            return list;
        }

    }
    }