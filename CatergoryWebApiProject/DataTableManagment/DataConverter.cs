using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.CustomType;
using System.Collections.Generic;
using System.Data;

namespace CatergoryWebApiProject.DataTableManagment
{
    public static class DataConverter
    {
        public static List<CategoryView> ConvertToList(DataTable dt)
        {
            List<CategoryView> result = new List<CategoryView>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result.Add(new CategoryView(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString()));
            }

            return result;
        }

        public static CategoryView ConvertToCategory(DataRow dr)
        {
            CategoryView result = new CategoryView(dr[1].ToString(), dr[3].ToString(), dr[5].ToString());

            return result;
        }
    }
}
