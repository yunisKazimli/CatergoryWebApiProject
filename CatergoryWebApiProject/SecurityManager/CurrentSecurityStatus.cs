using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatergoryWebApiProject.Models.UserTable;

namespace CatergoryWebApiProject.SecurityManager
{
    public static class CurrentSecurityStatus
    {
        public static UserTableModel User { get; private set; } = new UserTableModel(-1, "NULL", "NULL", 0);

        public static void Login(UserTableModel user)
        {
            User = user;
        }

        public static void Logout()
        {
            User = new UserTableModel(-1, "NULL", "NULL", 0);
        }
    }
}
