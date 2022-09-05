namespace CatergoryWebApiProject.CategoryTable.Models
{
    public class SubCategoryModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public SubCategoryModel(int id, string name)
        {
            SubCategoryId = id;

            SubCategoryName = name;
        }
    }
}
