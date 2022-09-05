using CatergoryWebApiProject.CategoryTableManager.DataTableManagment;
using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment;
using System.Data;

namespace CatergoryWebApiProject.ValidateManager
{
    public static class CategoryValidator
    {
        public static void NameTest(string[] names, CreateMode createMode, bool isNew)
        {
            NameFormatTest(names);

            if (isNew) IsNewName(names, createMode);
            else IsNameExist(names, createMode);
        }

        public static void NameTest(int Id, string NewName)
        {
            DataTable dt;

            NameFormatTest(new string[] { NewName });

            if (Id < 1000) IsNewName(new string[] { NewName }, CreateMode.NewBranch);

            Id /= 1000;

            dt = SqlConnector.ExQuery("SELECT * FROM CategoryTable WHERE (MainCategoryId = @Id AND CategoryName = @Name) OR (CategoryId = @Id AND SubCategoryName = @Name)",
                new string[]
                {
                    "@Id",
                    "@Name"
                },
                new string[]
                {
                    Id.ToString(),
                    NewName
                },
                new SqlDbType[]
                {
                    SqlDbType.Int,
                    SqlDbType.VarChar
                });

            if (dt.Rows.Count != 0) throw new AlreadyExistException();
        }

        private static void NameFormatTest(string[] names)
        {
            for (int i = 0; i < names.Length; i++) if (names[i] == null || names[i] == "") throw new EmptyParameterException();
        }

        public static void IdTest(int id)
        {
            IsIdTrueFormat(id);

            switch(id.ToString().Length)
            {
                case 3:

                    IsIdExist(id, CreateMode.NewBranch);

                    break;
                case 6:

                    IsIdExist(id, CreateMode.InMainCategory);

                    break;
                case 9:

                    IsIdExist(id, CreateMode.InCategory);

                    break;
            }
        }

        private static void IsIdExist(int id, CreateMode createMode)
        {
            DataTable dt = new DataTable();

            switch (createMode)
            {
                case CreateMode.NewBranch:

                    dt = CategoryTableController.RowsBy(id, "MainCategoryId");

                    break;
                case CreateMode.InMainCategory:

                    dt = CategoryTableController.RowsBy(id, "CategoryId");

                    break;
                case CreateMode.InCategory:

                    dt = CategoryTableController.RowsBy(id, "SubCategoryId");

                    break;
            }

            if (dt.Rows.Count == 0) throw new NotFoundByParameterException();
        }

        private static void IsIdTrueFormat(int id)
        {
            if ((id.ToString().Length != 3 && id.ToString().Length != 6 && id.ToString().Length != 9) || id < 0)
                throw new InvalidIdFormatException();
        }

        private static void IsNewName(string[] names, CreateMode createMode)
        {
            DataTable dt = GetDtByMode(names, createMode);

            if (dt.Rows.Count > 0) throw new AlreadyExistException();
        }

        private static void IsNameExist(string[] names, CreateMode createMode)
        {
            DataTable dt = GetDtByMode(names, createMode);

            if (dt.Rows.Count == 0) throw new NotFoundByParameterException();
        }

        private static DataTable GetDtByMode(string[] names, CreateMode createMode)
        {
            switch (createMode)
            {
                case CreateMode.NewBranch:

                    return CategoryTableController.RowsBy("MainCategoryName = @MainCategoryName",
                        new string[] { "@MainCategoryName" },
                        names,
                        new SqlDbType[] { SqlDbType.VarChar });

                case CreateMode.InMainCategory:

                    IsNameExist(new string[] { names[0] }, CreateMode.NewBranch);

                    return CategoryTableController.RowsBy("MainCategoryName = @MainCategoryName AND CategoryName = @CategoryName", 
                        new string[] { "@MainCategoryName", "@CategoryName" }, 
                        names, 
                        new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar });

                case CreateMode.InCategory:

                    IsNameExist(new string[] { names[0], names[1] }, CreateMode.InMainCategory);

                    return CategoryTableController.RowsBy("MainCategoryName = @MainCategoryName AND CategoryName = @CategoryName AND SubCategoryName = @SubCategoryName",
                        new string[] { "@MainCategoryName", "@CategoryName", "@SubCategoryName" },
                        names,
                        new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar });
            }

            return null;
        }
    }
}
