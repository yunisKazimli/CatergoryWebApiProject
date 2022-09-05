using CatergoryWebApiProject.CategoryTableManager.DataTableManagment;
using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.SecurityManager;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Mvc;

namespace CatergoryWebApiProject.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet("CategoryController/GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.User);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }
            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetAll()));
        }

        [HttpGet("CategoryController/GetByCategoryId")]
        public IActionResult GetByParameter(int Id)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.User);
                CategoryValidator.IdTest(Id);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetByParameter(Id)));
        }

        [HttpGet("CategoryController/GetByMainCategoryName")]
        public IActionResult GetByParameter(string MainCategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.User);
                CategoryValidator.NameTest(new string[] { MainCategoryName }, CreateMode.NewBranch, false);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetByParameter(MainCategoryName)));
        }

        [HttpGet("CategoryController/GetByCategoryName")]
        public IActionResult GetByParameter(string MainCategoryName, string CategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.User);
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName }, CreateMode.InMainCategory, false);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetByParameter(MainCategoryName, CategoryName)));
        }

        [HttpPost("CategoryController/CreateNewBranch")]
        public IActionResult CreateNewBranch(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.NewBranch, true);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.NewBranch)));
        }

        [HttpPost("CategoryController/CreateInMainCategory")]
        public IActionResult CreateInMainCategory(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InMainCategory, true);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.InMainCategory)));
        }

        [HttpPost("CategoryController/CreateInCategory")]
        public IActionResult CreateInCategory(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InCategory, true);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.InCategory)));
        }

        [HttpPut("CategoryController/Update")]
        public IActionResult Update(int Id, string NewName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.IdTest(Id);
                CategoryValidator.NameTest(Id, NewName);

            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Update(Id, NewName)));
        }

        [HttpDelete("CategoryController/DeleteById")]
        public IActionResult DeleteByParameter(int Id)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.IdTest(Id);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(Id)));
        }

        [HttpDelete("CategoryController/DeleteByMainCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.NameTest(new string[] { MainCategoryName }, CreateMode.NewBranch, false);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(MainCategoryName)));
        }

        [HttpDelete("CategoryController/DeleteByCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName, string CategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName }, CreateMode.InMainCategory, false);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(MainCategoryName, CategoryName)));
        }

        [HttpDelete("CategoryController/DeleteBySubCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Employee);
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InCategory, false);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(MainCategoryName, CategoryName, SubCategoryName)));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
