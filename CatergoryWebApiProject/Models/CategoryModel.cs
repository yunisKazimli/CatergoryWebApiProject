using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CatergoryWebApiProject.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryModel> SubCategoryList { get; set; }

        public CategoryModel(int id, string name, List<SubCategoryModel> list)
        {
            CategoryId = id;

            CategoryName = name;

            SubCategoryList = list;
        }
    }
}
