using CatergoryWebApiProject.Models.UserTable;
using CatergoryWebApiProject.SecurityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.DataTableManagment.UserTableManager
{
    public static class UserTableController
    {
        public static DataTable GetAll()
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT *
                FROM UserTable
                ");

            return dt;
        }

        public static DataTable GetByParameter(int Id)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT *
                FROM UserTable
                WHERE Id = @Id
                ",
                new string[]
                {
                    "@Id"
                },
                new string[]
                {
                    Id.ToString()
                },
                new SqlDbType[]
                {
                    SqlDbType.Int
                });

            return dt;
        }

        public static DataTable GetByParameter(string Name)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT *
                FROM UserTable
                WHERE Name = @Name
                ",
                new string[]
                {
                    "@Name"
                },
                new string[]
                {
                    Name
                },
                new SqlDbType[]
                {
                    SqlDbType.VarChar
                });

            return dt;
        }

        public static DataTable GetByParameter(AccessLevelType AccessLevel)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT *
                FROM UserTable
                WHERE AccessLevel = @AccessLevel
                ",
                new string[]
                {
                    "@AccessLevel"
                },
                new string[]
                {
                    ((int)AccessLevel).ToString()
                },
                new SqlDbType[]
                {
                    SqlDbType.Int
                });

            return dt;
        }

        public static DataTable GetByParameter(UserTableModel user)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT *
                FROM UserTable
                WHERE 
                Id = @Id AND
                Name = @Name AND
                Password = @Password AND
                AccessLevel = @AccessLevel
                ",
                new string[]
                {
                    "@Id",
                    "@Name",
                    "@Password",
                    "@AccessLevel"
                },
                new string[]
                {
                    user.Id.ToString(),
                    user.Name,
                    user.Password,
                    ((int)user.AccessLevel).ToString()
                },
                new SqlDbType[]
                {
                    SqlDbType.Int,
                    SqlDbType.VarChar,
                    SqlDbType.VarChar,
                    SqlDbType.Int
                });

            return dt;
        }

        public static DataTable Create(string Name, string Password, AccessLevelType AccessLevel)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"INSERT INTO UserTable 
                VALUES(@Name, @Password, @AccessLevel);
                SELECT TOP 1 *
                FROM UserTable
                ORDER BY Id DESC;",
                new string[]
                { 
                    "@Name",
                    "@Password",
                    "@AccessLevel"
                },
                new string[]
                { 
                    Name,
                    Password,
                    ((int)AccessLevel).ToString()
                },
                new SqlDbType[]
                {
                    SqlDbType.VarChar,
                    SqlDbType.VarChar,
                    SqlDbType.Int
                });

            return dt;
        }

        public static DataTable Update(UserTableModel user)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"UPDATE UserTable
                SET Name = @Name, Password = @Password, AccessLevel = @AccessLevel
                WHERE Id = @Id
                SELECT *
                FROM UserTable
                WHERE Id = @Id
                ",
                new string[]
                {
                    "@Id",
                    "@Name",
                    "@Password",
                    "@AccessLevel"
                },
                new string[]
                {
                    user.Id.ToString(),
                    user.Name,
                    user.Password,
                    ((int)user.AccessLevel).ToString()
                },
                new SqlDbType[]
                {
                    SqlDbType.Int,
                    SqlDbType.VarChar,
                    SqlDbType.VarChar,
                    SqlDbType.Int
                });

            return dt;
        }

        public static DataTable Delete(int Id)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT *
                FROM UserTable
                WHERE Id = @Id;
                DELETE FROM UserTable
                WHERE Id = @Id
                ",
                new string[]
                {
                    "@Id"
                },
                new string[]
                {
                    Id.ToString()
                },
                new SqlDbType[]
                {
                    SqlDbType.Int
                });

            return dt;
        }
    }
}
