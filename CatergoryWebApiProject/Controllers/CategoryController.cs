using CatergoryWebApiProject.CategoryTableManager.DataTableManagment;
using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.SecurityManager;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatergoryWebApiProject.Controllers
{
    [Authorize("Bearer")]
    public class CategoryController : Controller
    {
        [Authorize(Roles = "Admin, Employee, User")]
        [HttpGet("CategoryController/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetAll()));
        }

        [Authorize(Roles = "Admin, Employee, User")]
        [HttpGet("CategoryController/GetByCategoryId")]
        public IActionResult GetByParameter(int Id)
        {
            try
            {
                CategoryValidator.IdTest(Id);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetByParameter(Id)));
        }

        [Authorize(Roles = "Admin, Employee, User")]
        [HttpGet("CategoryController/GetByMainCategoryName")]
        public IActionResult GetByParameter(string MainCategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName }, CreateMode.NewBranch, false);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetByParameter(MainCategoryName)));
        }

        [Authorize(Roles = "Admin, Employee, User")]
        [HttpGet("CategoryController/GetByCategoryName")]
        public IActionResult GetByParameter(string MainCategoryName, string CategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName }, CreateMode.InMainCategory, false);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.GetByParameter(MainCategoryName, CategoryName)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost("CategoryController/CreateNewBranch")]
        public IActionResult CreateNewBranch(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.NewBranch, true);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.NewBranch)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost("CategoryController/CreateInMainCategory")]
        public IActionResult CreateInMainCategory(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InMainCategory, true);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.InMainCategory)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost("CategoryController/CreateInCategory")]
        public IActionResult CreateInCategory(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InCategory, true);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.InCategory)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPut("CategoryController/Update")]
        public IActionResult Update(int Id, string NewName)
        {
            try
            {
                CategoryValidator.IdTest(Id);
                CategoryValidator.NameTest(Id, NewName);

            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.Update(Id, NewName)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("CategoryController/DeleteById")]
        public IActionResult DeleteByParameter(int Id)
        {
            try
            {
                CategoryValidator.IdTest(Id);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(Id)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("CategoryController/DeleteByMainCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName }, CreateMode.NewBranch, false);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(MainCategoryName)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("CategoryController/DeleteByCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName, string CategoryName)
        {
            try
            {
                CategoryValidator.NameTest(new string[] { MainCategoryName, CategoryName }, CreateMode.InMainCategory, false);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(CategoryTableConverter.ConvertToList(CategoryTableController.DeleteByParameter(MainCategoryName, CategoryName)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("CategoryController/DeleteBySubCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
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
