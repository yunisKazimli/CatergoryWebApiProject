using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CatergoryWebApiProject.DataTableManagment
{
    public static class DataConverter
    {
        public static CategoryTableModel ConvertToList(DataTable dt)
        {
            CategoryTableModel result = new CategoryTableModel
                (
                    (
                        from mainC in new DataView(dt).ToTable(true, "MainCategoryId", "MainCategoryName").AsEnumerable()
                        select new MainCategoryModel
                        (
                            Convert.ToInt32(mainC["MainCategoryId"]),
                            mainC["MainCategoryName"].ToString(),
                            (
                                from C in new DataView(dt).ToTable(true, "MainCategoryId", "CategoryId", "CategoryName").AsEnumerable().Where(el => el["MainCategoryId"].ToString() == mainC["MainCategoryId"].ToString())
                                select new CategoryModel
                                (
                                    Convert.ToInt32(C["CategoryId"]),
                                    C["CategoryName"].ToString(),
                                    (
                                        from subC in new DataView(dt).ToTable(true, "CategoryId", "SubCategoryId", "SubCategoryName").AsEnumerable().Where(el => el["CategoryId"].ToString() == C["CategoryId"].ToString())
                                        select new SubCategoryModel
                                        (
                                            Convert.ToInt32(subC["SubCategoryId"]),
                                            subC["SubCategoryName"].ToString()

                                        )
                                    ).ToList()
                                )
                            ).ToList()
                        )
                    ).ToList()
                );

            return result;
        }
    }
}
