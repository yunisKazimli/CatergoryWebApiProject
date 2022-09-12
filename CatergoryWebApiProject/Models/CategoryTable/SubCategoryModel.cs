namespace CatergoryWebApiProject.CategoryTable.Models
{
    public class SubCategoryModel
    {
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public SubCategoryModel(int userId, int id, string name)
        {
            UserId = userId;

            SubCategoryId = id;

            SubCategoryName = name;
        }
    }
}
