using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.Models.UserTable;
using CatergoryWebApiProject.SecurityManager;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("AdminController/GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Admin);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.GetAll()));
        }

        [HttpGet("AdminController/GetById")]
        public IActionResult GetByParameter(int Id)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Admin);
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
                SecurityController.Authorize(AccessLevelType.Admin);
                UserValidator.NameTest(name, false);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.GetByParameter(name)));
        }

        [HttpGet("AdminController/GetByAccessLevel")]
        public IActionResult GetByParameter(AccessLevelType AccessLevel)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Admin);
                UserValidator.AccessLevelTest(AccessLevel);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.GetByParameter(AccessLevel)));
        }

        [HttpPost("AdminController/CreateUser")]
        public IActionResult Create(string Name, string password, AccessLevelType AccessLevel)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Admin);
                UserValidator.UserTest(Name, password, AccessLevel, true);
            }

            catch(BaseException e)
            {
                return Problem(e.ToString());
            }
            string a = PasswordManager.PasswordHash(password);
            return Ok(UserTableConverter.ConvertToList(UserTableController.Create(Name, a, AccessLevel)));
        }

        [HttpPut("AdminController/Update")]
        public IActionResult Set(int Id, string Name, string password, AccessLevelType AccessLevel)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Admin);
                UserValidator.IdTest(Id);
                UserValidator.UserTest(Name, password, AccessLevel, true);
            }

            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(UserTableConverter.ConvertToList(UserTableController.Update(new UserTableModel(Id, Name, PasswordManager.PasswordHash(password), AccessLevel)))) ;
        }

        [HttpDelete("AdminController/Delete")]
        public IActionResult Delete (int Id)
        {
            try
            {
                SecurityController.Authorize(AccessLevelType.Admin);
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
