using System.Data;

namespace CatergoryWebApiProject.DataTableManagment
{
    public enum CreateMode
    {
        NewBranch,
        InMainCategory,
        InCategory
    }

    public static class DataTableController
    {
        public static DataTable GetAll()
        {
            DataTable dt = SqlConnector.ExQuery("SELECT * FROM CategoryTable ORDER BY MainCategoryId, CategoryId, SubCategoryId");

            return dt;
        }

        public static DataTable GetByParameter(string MainCategoryName)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT * 
                FROM CategoryTable 
                WHERE 
                MainCategoryName = @MainCategoryName",
                new string[] 
                {
                    "@MainCategoryName"
                }, 
                new string[] 
                {
                    MainCategoryName
                }, 
                new SqlDbType[] 
                { 
                    SqlDbType.VarChar
                }
                );

            return dt;
        }
 
        public static DataTable GetByParameter(string MainCategoryName, string CategoryName)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT * 
                FROM CategoryTable 
                WHERE 
                MainCategoryName = @MainCategoryName AND
                CategoryName = @CategoryName",
                new string[] 
                {
                    "@MainCategoryName",
                    "@CategoryName"
                }, 
                new string[] 
                {
                    MainCategoryName,
                    CategoryName
                }, 
                new SqlDbType[] 
                { 
                SqlDbType.VarChar,
                SqlDbType.VarChar
                }
                );

            return dt;
        }
   
        public static DataTable GetByParameter(int Id)
        {
            DataTable dt = SqlConnector.ExQuery(
                @"SELECT * 
                FROM CategoryTable 
                WHERE 
                MainCategoryId = @Id OR
                CategoryId = @Id OR
                SubCategoryId = @Id",
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
                }
                );

            return dt;
        }

        public static DataRow Create(string MainCategoryName, string CategoryName, string SubCategoryName, CreateMode createMode)
        {
            DataTable dt = new DataTable();
            DataTable idDt = NewId(MainCategoryName, CategoryName, SubCategoryName, createMode);

            dt = SqlConnector.ExQuery(
                @"INSERT INTO CategoryTable 
                VALUES(
                    @MainCategoryId, 
                    @MainCategoryName,
                    @CategoryId, 
                    @CategoryName, 
                    @SubCategoryId, 
                    @SubCategoryName
                )
                SELECT *
                FROM CategoryTable
                WHERE SubCategoryId = @SubCategoryId",
                new string[]
                {
                    "@MainCategoryID",
                    "@MainCategoryName",
                    "@CategoryId",
                    "@CategoryName",
                    "@SubCategoryId",
                    "@SubCategoryName"
                },
                new string[]
                {
                    idDt.Rows[0][0].ToString(),
                    MainCategoryName,
                    idDt.Rows[0][1].ToString(),
                    CategoryName,
                    idDt.Rows[0][2].ToString(),
                    SubCategoryName
                },
                new SqlDbType[]
                {
                    SqlDbType.Int,
                    SqlDbType.VarChar,
                    SqlDbType.Int,
                    SqlDbType.VarChar,
                    SqlDbType.Int,
                    SqlDbType.VarChar
                }
                );

            return dt.Rows[0];
        }

        public static DataTable RowsBy(int id, string columnName)
        {
            return SqlConnector.ExQuery(
                @$"SELECT * 
                FROM CategoryTable
                WHERE {columnName} = @{columnName}
                ",
                new string[] { columnName }, new string[] { id.ToString() }, new SqlDbType[] { SqlDbType.Int }
                );
        }

        public static DataTable RowsBy(string condition, string[] columnsName, string[] columnsValue, SqlDbType[] coumnsType)
        {
            return SqlConnector.ExQuery(
                $@"SELECT * 
                FROM CategoryTable
                WHERE {condition}
                ",
                columnsName, columnsValue, coumnsType);
        }

        private static DataTable NewId(string MainCategoryName, string CategoryName, string SubCategoryName,  CreateMode createMode)
        {
            DataTable idDt = new DataTable();

            switch (createMode) {

                case CreateMode.NewBranch:
                    idDt = SqlConnector.ExQuery(
                        @"DECLARE @MainCategoryId AS INT;
                        SELECT @MainCategoryId = MAX(MainCategoryId)
                        FROM CategoryTable;

                        IF (@MainCategoryId IS NULL)
                        BEGIN
                            SELECT @MainCategoryId = 100;
                        END
                        ELSE 
                        BEGIN
                            SELECT @MainCategoryId = @MainCategoryId + 1;
                        END
                        SELECT 
                        @MainCategoryId,
                        (@MainCategoryId * 1000 + 100),
                        ((@MainCategoryId * 1000 + 100)*1000 + 100)");
                    break;
                case CreateMode.InMainCategory:
                    idDt = SqlConnector.ExQuery(
                        @"DECLARE @MainCategoryId AS INT;
                        DECLARE @CategoryId AS INT;

                        SELECT @MainCategoryId = MainCategoryId  
                        FROM CategoryTable
                        WHERE @MainCategoryName = MainCategoryName;

                        SELECT @CategoryId = MAX(CategoryId)
                        FROM CategoryTable
                        WHERE MainCategoryId = @MainCategoryId;
                
                        SELECT @CategoryId = @CategoryId + 1;
        
                        SELECT 
                        @MainCategoryId,
                        @CategoryId,
                        (@CategoryId*1000 + 100)",
                        new string[]
                        {
                            "@MainCategoryName"
                        },
                        new string[]
                        {
                            MainCategoryName
                        },
                        new SqlDbType[]
                        {
                            SqlDbType.VarChar
                        }
                        );
                    break;
                case CreateMode.InCategory:
                    idDt = SqlConnector.ExQuery(
                        @"DECLARE @MainCategoryId AS INT;
                        DECLARE @CategoryId AS INT;
                        DECLARE @SubCategoryId AS INT;

                        SELECT 
                        @MainCategoryId = MainCategoryId,
                        @CategoryId = CategoryId
                        FROM CategoryTable
                        WHERE @MainCategoryName = MainCategoryName AND
                        @CategoryName = CategoryName;

                        SELECT @SubCategoryId = MAX(SubCategoryId)
                        FROM CategoryTable
                        WHERE CategoryId = @CategoryId;
                
                        SELECT @SubCategoryId = @SubCategoryId + 1;
        
                        SELECT 
                        @MainCategoryId,
                        @CategoryId,
                        @SubCategoryId",
                        new string[]
                        {
                            "@MainCategoryName",
                            "@CategoryName"
                        },
                        new string[]
                        {
                            MainCategoryName,
                            CategoryName
                        },
                        new SqlDbType[]
                        {
                            SqlDbType.VarChar,
                            SqlDbType.VarChar
                        }
                        );
                    break;
            }

            return idDt;
        }
    }
}
