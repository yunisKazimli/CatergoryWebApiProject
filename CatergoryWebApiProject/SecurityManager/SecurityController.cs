using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.Models.UserTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.SecurityManager
{
    public enum AccessLevelType
    { 
        Passive,//cannot anything
        User,//can get category table
        Employee,//can get,set category table
        Admin//can get,set category,user table
    }

    public static class SecurityController
    {
        public static void Autentificate(UserTableModel user, string Password, bool isIn)
        {
            if (!PasswordManager.PasswordEqual(user.Password, Password)) throw new InvalidPasswordException();

            if (isIn) CurrentSecurityStatus.Login(user);
            else CurrentSecurityStatus.Logout();
        }

        public static void Authorize(AccessLevelType accessLevel)
        {
            if (accessLevel > CurrentSecurityStatus.User.AccessLevel) throw new AccessDeniedException();
        }
    }
}
