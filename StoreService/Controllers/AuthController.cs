using DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreService.IServices.Auth;

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
        public async Task<IActionResult> SignIn([FromBody] DTOSignIn request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var respone = await authService.SignIn(request);
            if (respone.IsSuccess)
            {
                return Ok(respone);
            }
            else
            {
                return BadRequest(respone);
            }
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] DTOSignUp request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var respone = await authService.SignUp(request);
            if (respone.IsSuccess)
            {
                return Ok(respone);
            }
            else
            {
                return BadRequest(respone);
            }
        }
    }
}
