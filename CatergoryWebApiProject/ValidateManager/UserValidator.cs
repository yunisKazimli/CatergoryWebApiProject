using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.Models.UserTable;
using CatergoryWebApiProject.SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.ValidateManager
{
    public static class UserValidator
    {
        public static void UserTest(string Name, string Password, AccessLevelType AccessLevel, bool isNew)
        {
            NameTest(Name, isNew);

            PasswordTest(Password);

            AccessLevelTest(AccessLevel);
        }

        public static void NameTest(string Name, bool isNew)
        {
            if (Name == null || Name == "") throw new EmptyParameterException();

            if (isNew) IsNameNew(Name);
            else IsNameExist(Name);
        }

        public static void UserLoginned(string name, string password)
        {
            if (CurrentSecurityStatus.User.Name != name || !PasswordManager.PasswordEqual(CurrentSecurityStatus.User.Password, password)) throw new UserNotLoginnedException();
        }

        public static void PasswordTest(string password)
        {
            if (password == null || password == "") throw new EmptyParameterException();
        }

        public static void IdTest(int Id)
        {
            if (UserTableController.GetByParameter(Id).Rows.Count == 0) throw new NotFoundByParameterException();
        }

        public static void AccessLevelTest(AccessLevelType AccessLevel)
        {

        }

        private static void IsNameNew(string Name)
        {
            if (UserTableController.GetByParameter(Name).Rows.Count > 0) throw new AlreadyExistException();
        }

        private static void IsNameExist(string Name)
        {
            if (UserTableController.GetByParameter(Name).Rows.Count == 0) throw new NotFoundByParameterException();
        }
    }
}
