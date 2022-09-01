using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CatergoryWebApiProject.Models
{
    public class MainCategoryModel
    {
        public int MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
        public List<CategoryModel> CategoryList { get; set; }

        public MainCategoryModel(int id, string name, List<CategoryModel> list)
        {
            MainCategoryId = id;

            MainCategoryName = name;

            CategoryList = list;
        }
    }
}
