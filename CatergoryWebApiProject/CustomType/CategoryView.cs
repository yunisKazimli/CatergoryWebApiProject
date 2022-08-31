using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.CustomType
{
    public interface IcategoryView
    {
        public int FullId { get; set; }
        public string MainCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }


    public class CategoryView : IcategoryView
    {
        public int FullId { get; set; }
        public string MainCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public CategoryView(int fullId, string mainCategoryName, string categoryName, string subCategoryName)
        {
            FullId = fullId;
            MainCategoryName = mainCategoryName;
            CategoryName = categoryName;
            SubCategoryName = subCategoryName;
        }
    }
}
