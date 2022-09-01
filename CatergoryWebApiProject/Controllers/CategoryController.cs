using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Mvc;

namespace CatergoryWebApiProject.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(DataConverter.ConvertToList(DataTableController.GetAll()));
        }
        
        [HttpGet("GetByCategoryId")]
        public IActionResult GetByParameter(int Id)
        {
            try
            {
                Validator.IdTest(Id);
            }

            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.GetByParameter(Id)));
        }

        [HttpGet("GetByMainCategoryName")]
        public IActionResult GetByParameter(string MainCategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName }, CreateMode.NewBranch, false);
            }

            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.GetByParameter(MainCategoryName)));
        }

        [HttpGet("GetByCategoryName")]
        public IActionResult GetByParameter(string MainCategoryName, string CategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName, CategoryName }, CreateMode.InMainCategory, false);
            }

            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.GetByParameter(MainCategoryName, CategoryName)));
        }

        [HttpPost("CreateNewBranch")]
        public IActionResult CreateNewBranch(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.NewBranch, true);
            }
            catch(InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.NewBranch)));
        }

        [HttpPost("CreateInMainCategory")]
        public IActionResult CreateInMainCategory(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InMainCategory, true);
            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.InMainCategory)));
        }

        [HttpPost("CreateInCategory")]
        public IActionResult CreateInCategory(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InCategory, true);
            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.Create(MainCategoryName, CategoryName, SubCategoryName, CreateMode.InCategory)));
        }

        [HttpPut("Update")]
        public IActionResult Update(int Id, string NewName)
        {
            try
            {
                Validator.IdTest(Id);
                Validator.NameTest(Id, NewName);

            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.Update(Id, NewName)));
        }

        [HttpDelete("DeleteById")]
        public IActionResult DeleteByParameter(int Id)
        {
            try
            {
                Validator.IdTest(Id);
            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.DeleteByParameter(Id)));
        }

        [HttpDelete("DeleteByMainCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName }, CreateMode.NewBranch, false);
            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.DeleteByParameter(MainCategoryName)));
        }

        [HttpDelete("DeleteByCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName, string CategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName, CategoryName }, CreateMode.InMainCategory, false);
            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.DeleteByParameter(MainCategoryName, CategoryName)));
        }

        [HttpDelete("DeleteBySubCategoryName")]
        public IActionResult DeleteByParameter(string MainCategoryName, string CategoryName, string SubCategoryName)
        {
            try
            {
                Validator.NameTest(new string[] { MainCategoryName, CategoryName, SubCategoryName }, CreateMode.InCategory, false);
            }
            catch (InvalidParameterException e)
            {
                return Problem(e.ToString());
            }

            return Ok(DataConverter.ConvertToList(DataTableController.DeleteByParameter(MainCategoryName, CategoryName, SubCategoryName)));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
