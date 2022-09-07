using CatergoryWebApiProject.Models.UserTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.JwtTokenManagment
{
    public interface IJwtTokenManager
    {
        public string CreateToken(UserTableModel user);
    }
}
