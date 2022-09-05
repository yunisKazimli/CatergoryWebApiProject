using CatergoryWebApiProject.SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.Models.UserTable
{
    public class UserTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public AccessLevelType AccessLevel { get; set; }

        public UserTableModel(int _Id, string _Name, string _Password, AccessLevelType _AccessLevel)
        {
            Id = _Id;
            Name = _Name;
            Password = _Password;
            AccessLevel = _AccessLevel;
        }
    }
}
