using CatergoryWebApiProject.CustomException;
using CatergoryWebApiProject.DataTableManagment;
using CatergoryWebApiProject.DataTableManagment.UserTableManager;
using CatergoryWebApiProject.JwtTokenManagment;
using CatergoryWebApiProject.SecurityManager;
using CatergoryWebApiProject.ValidateManager;
using Microsoft.AspNetCore.Mvc;

namespace CatergoryWebApiProject.Controllers
{
    public class UserController : Controller
    {
        public IJwtTokenManager _tokenManager;

        public UserController(IJwtTokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost("UserController/Login")]
        public IActionResult Login(string Name, string Password)
        {
            try
            {
                UserValidator.NameTest(Name, false);
                UserValidator.PasswordTest(Password);
                UserValidator.Authenticate(Name, Password);
            }
            catch(BaseException e)
            {
                return Problem(e.ToString());
            }

            return Ok(_tokenManager.CreateToken(UserTableConverter.ConvertToUser(Name)));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
