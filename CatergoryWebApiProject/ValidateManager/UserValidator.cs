using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.Models.UserTable;
using CatergoryWebApiProject.SecurityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.ValidateManager
{
    public static class UserValidator
    {
        public static void NameTest(string Name, bool isNew)
        {
            if (Name == null || Name == "") throw new EmptyParameterException();

            if (isNew) IsNameNew(Name);
            else IsNameExist(Name);
        }

        public static void PasswordTest(string password)
        {
            if (password == null || password == "") throw new EmptyParameterException();
        }

        public static void IdTest(int Id)
        {
            if (UserTableController.GetByParameter(Id).Rows.Count == 0) throw new NotFoundByParameterException();
        }

        public static void Authenticate(string name, string password)
        {
            if (!PasswordManager.PasswordEqual(UserTableConverter.ConvertToUser(UserTableController.GetByParameter(name).Rows[0]).Password, password)) throw new InvalidPasswordException();
        }

        private static void IsNameNew(string Name)
        {
            if (UserTableController.GetByParameter(Name).Rows.Count > 0) throw new AlreadyExistException();
        }

        private static void IsNameExist(string Name)
        {
            DataRowCollection d = UserTableController.GetByParameter(Name).Rows;
            if (d.Count == 0) throw new NotFoundByParameterException();
        }
    }
}
