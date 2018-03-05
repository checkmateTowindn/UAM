using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
namespace CM.Common.MySQL
{
    public static class MySQLConnection
    {
        private static string connectionString = @"Host=slightcold.date;Port=3306;Username=root;Password=qaz123456;Database=Witkey;Charset=utf8";
        private static Queue<MySqlConnection> connectionQueue = new Queue<MySqlConnection>();
        private static MySqlConnection baseConnection = new MySqlConnection(connectionString);
        private static  MySqlCommand baseCommand = new MySqlCommand();

        public static MySqlConnection GetMySqlConnection()
        {
            if (connectionQueue.Count > 0)
            {
                lock (connectionQueue)
                {
                    if (connectionQueue.Count > 0)
                    {
                        return connectionQueue.Dequeue();
                    }
                    else
                    {
                        MySqlConnection conection = (MySqlConnection)baseConnection.Clone();
                        conection.Open();
                        return conection;
                    }
                }
            }
            else
            {
                MySqlConnection conection = (MySqlConnection)baseConnection.Clone();
                conection.Open();
                return conection;
            }
        }

        public static bool ReturnConnection(MySqlConnection mysqlConnection)
        {
            connectionQueue.Enqueue(mysqlConnection);
            return true;
        }

        public static MySqlCommand GetMySqlCommand(MySqlConnection connection,String sql, MySqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            MySqlCommand commond =(MySqlCommand)baseCommand.Clone();
            commond.Connection = connection;
            commond.CommandText = sql;
            commond.CommandType = commandType;
            if(parameters != null) { commond.Parameters.AddRange(parameters); }

            return commond;
        }
    }
}
