using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.CustomType
{
    public interface IcategoryView
    {
        public string MainCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }


    public class CategoryView : IcategoryView
    {
        public string MainCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public CategoryView(string mainCategoryName, string categoryName, string subCategoryName)
        {
            MainCategoryName = mainCategoryName;
            CategoryName = categoryName;
            SubCategoryName = subCategoryName;
        }
    }
}
