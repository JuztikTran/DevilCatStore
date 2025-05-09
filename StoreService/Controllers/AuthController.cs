using DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreService.IServices.Auth;
using StoreService.IServices.Utils;

namespace StoreService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService service)
        {
            this.authService = service;
        }


        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] DTOSignIn request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var respone = await authService.SignIn(request);
            return respone.IsSuccess ? Ok(respone) : Unauthorized(respone);
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] DTOSignUp request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var respone = await authService.SignUp(request);
            return respone.IsSuccess ? Ok(respone) : BadRequest(respone);
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(DTOForgotPassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var respone = await authService.ForgotPassword(request);
            return respone.IsSuccess ? Ok(respone) : BadRequest(respone);
        }
    }
}
