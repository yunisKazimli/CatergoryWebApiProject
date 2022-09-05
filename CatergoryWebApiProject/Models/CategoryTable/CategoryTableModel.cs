using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CatergoryWebApiProject.CategoryTable.Models
{
    public class CategoryTableModel
    {
        public List<MainCategoryModel> MainCategoryList { get; set; }

        public CategoryTableModel(List<MainCategoryModel> list)
        {
            MainCategoryList = list;
        }
    }
}
