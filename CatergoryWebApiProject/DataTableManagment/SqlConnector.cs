using System;
using System.Collections.Generic;
using System.Data;
using CatergoryWebApiProject.CustomException;
using Microsoft.Data.SqlClient;

namespace CatergoryWebApiProject.DataTableManagment
{
    static class SqlConnector
    {
        public static SqlConnection sqlc;

        public static void Init(string serverParameter)
        {
            sqlc = new SqlConnection(serverParameter);
        }
        
        public static DataTable ExQuery(string commandText, string[] paramNames, string[] paramValues, SqlDbType[] paramTypes)
        {
            DataTable dt = new DataTable();

            using (SqlCommand command = sqlc.CreateCommand())
            {
                sqlc.Open();

                for (int i = 0; i < paramNames.Length; i++)
                {
                    command.Parameters.Add(paramNames[i].Split('_')[0], paramTypes[i]);
                    command.Parameters[i].Value = paramValues[i];
                }

                command.CommandText = commandText;

                dt.Load(command.ExecuteReader());

                sqlc.Close();
            }

            return dt;
        }

        public static DataTable ExQuery(string commandText)
        {
            return ExQuery(commandText, new string[] { }, new string[] { }, new SqlDbType[] { });
        }

        public static void ExNonQuery(string commandText, string[] paramNames, string[] paramValues, SqlDbType[] paramTypes)
        {
            using (SqlCommand command = sqlc.CreateCommand())
            {
                sqlc.Open();

                for (int i = 0; i < paramNames.Length; i++)
                {
                    command.Parameters.Add(paramNames[i].Split('_')[0], paramTypes[i]);
                    command.Parameters[i].Value = paramValues[i];
                }

                command.CommandText = commandText;
                command.ExecuteNonQuery();

                sqlc.Close();
            }
        }

        public static void ExNonQuery(string commandText)
        {
            ExNonQuery(commandText, new string[] { }, new string[] { }, new SqlDbType[] { });
        }
    }
}
