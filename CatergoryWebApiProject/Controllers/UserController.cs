using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.SecurityManager;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject.Controllers
{
    public class UserController : Controller
    {
        [HttpPost("UserController/Login")]
        public IActionResult Login(string Name, string Password)
        {
            try
            {
                UserValidator.NameTest(Name, false);
                UserValidator.PasswordTest(Password);
                SecurityController.Autentificate(UserTableConverter.ConvertToUser(Name), Password, true);
            }
            catch(BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok("Login successed");
        }

        [HttpPost("UserController/Logout")]
        public IActionResult Logout(string Name, string Password)
        {
            try
            {
                UserValidator.NameTest(Name, false);
                UserValidator.PasswordTest(Password);
                UserValidator.UserLoginned(Name, Password);
                SecurityController.Autentificate(UserTableConverter.ConvertToUser(Name), Password, false);
            }
            catch (BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok("Logout successed");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
