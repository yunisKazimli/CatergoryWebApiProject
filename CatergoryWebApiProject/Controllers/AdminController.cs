using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.Models.UserTable;
using CatergoryWebApiProject.SecurityManager;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatergoryWebApiProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize("Bearer")]
    public class AdminController : Controller
    {
        [HttpGet("AdminController/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(UserTableConverter.ConvertToList(UserTableController.GetAll()));
        }

        [HttpGet("AdminController/GetById")]
        public IActionResult GetByParameter(int Id)
        {
            try
            {
                UserValidator.IdTest(Id);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.GetByParameter(Id)));
        }

        [HttpGet("AdminController/GetByName")]
        public IActionResult GetByParameter(string name)
        {
            try
            {
                UserValidator.NameTest(name, false);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.GetByParameter(name)));
        }

        [HttpPost("AdminController/CreateUser")]
        public IActionResult Create(string Name, string password, AccessLevelType AccessLevel)
        {
            try
            {
                UserValidator.NameTest(Name, true);
                UserValidator.PasswordTest(password);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }
            string a = PasswordManager.PasswordHash(password);
            return Ok(UserTableConverter.ConvertToList(UserTableController.Create(Name, a, AccessLevel)));
        }

        [HttpPut("AdminController/UpdateName")]
        public IActionResult SetName(int Id, string Name)
        {
            try
            {
                UserValidator.IdTest(Id);
                UserValidator.NameTest(Name, true);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            UserTableModel user = UserTableConverter.ConvertToUser(UserTableController.GetByParameter(Id).Rows[0]);

            return Ok(UserTableConverter.ConvertToList(UserTableController.Update(new UserTableModel(user.Id, Name, user.Password, user.AccessLevel))));
        }

        [HttpPut("AdminController/UpdatePassword")]
        public IActionResult SetPassword(int Id, string Password)
        {
            try
            {
                UserValidator.IdTest(Id);
                UserValidator.PasswordTest(Password);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            UserTableModel user = UserTableConverter.ConvertToUser(UserTableController.GetByParameter(Id).Rows[0]);

            return Ok(UserTableConverter.ConvertToList(UserTableController.Update(new UserTableModel(user.Id, user.Name, PasswordManager.PasswordHash(Password), user.AccessLevel))));
        }

        [HttpPut("AdminController/UpdateAccessLevel")]
        public IActionResult SetAccessLevel(int Id, AccessLevelType AccessLevel)
        {
            try
            {
                UserValidator.IdTest(Id);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            UserTableModel user = UserTableConverter.ConvertToUser(UserTableController.GetByParameter(Id).Rows[0]);

            return Ok(UserTableConverter.ConvertToList(UserTableController.Update(new UserTableModel(user.Id, user.Name, user.Password, AccessLevel))));
        }

        [HttpDelete("AdminController/Delete")]
        public IActionResult Delete (int Id)
        {
            try
            {
                UserValidator.IdTest(Id);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.Delete(Id)));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
