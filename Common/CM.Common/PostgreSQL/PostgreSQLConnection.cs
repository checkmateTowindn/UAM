using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CM.Common.PostgreSQL
{
    public static class PostgreSQLConnection
    {
        private static string connectionString = @"Host=slightcold.date;Port=5432;Username=postgres;Password=qaz123456;Database=UAM";
        private static Queue<NpgsqlConnection> connectionQueue = new Queue<NpgsqlConnection>();
        private static NpgsqlConnection baseConnection = new NpgsqlConnection(connectionString);
        private static NpgsqlCommand baseCommand = new NpgsqlCommand();

        public static NpgsqlConnection GetNpgsqlConnection()
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
                        var conection = baseConnection.CloneWith(connectionString);
                        conection.Open();
                        return conection;
                    }
                }
            }
            else
            {
                var conection = baseConnection.CloneWith(connectionString);
                conection.Open();
                return conection;
            }
        }

        public static bool ReturnConnection(NpgsqlConnection npgsqlConnection)
        {
            connectionQueue.Enqueue(npgsqlConnection);
            return true;
        }

        public static NpgsqlCommand GetNpgsqlCommand(NpgsqlConnection connection,String sql, NpgsqlParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            var commond = baseCommand.Clone();
            commond.Connection = connection;
            commond.CommandText = sql;
            commond.CommandType = commandType;
            if(parameters != null) { commond.Parameters.AddRange(parameters); }

            return commond;
        }
    }
}
