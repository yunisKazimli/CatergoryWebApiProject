using CatergoryWebApiProject.Models.UserTable;
using CatergoryWebApiProject.SecurityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.DataTableManagment.UserTableManager
{
    public class UserTableConverter
    {
        public static List<UserTableModel> ConvertToList(DataTable dt)
        {
            List<UserTableModel> userTableList = new List<UserTableModel>();

            return (from u in dt.AsEnumerable() select new UserTableModel((int)u["Id"], u["Name"].ToString(), u["Password"].ToString(), (AccessLevelType)u["AccessLevel"])).ToList();
        }

        public static UserTableModel ConvertToUser(DataRow dr)
        {
            return new UserTableModel((int)dr["Id"], dr["Name"].ToString(), dr["Password"].ToString(), (AccessLevelType)dr["AccessLevel"]);
        }

        public static UserTableModel ConvertToUser(string Name)
        {
            return ConvertToUser(UserTableController.GetByParameter(Name).Rows[0]);
        }
    }
}
