using DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace StoreClient.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {
        [HttpGet("/sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet("/sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet("/forgot-password")]
        public IActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost("/sign-in")]
        public IActionResult SignIn(DTOSignIn data)
        {
            return View();
        }

        [HttpPost("/sign-up")]
        public IActionResult SignUp(DTOSignUp data)
        {
            return View();
        }


    }
}
