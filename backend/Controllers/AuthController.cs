using backend.Services;
using backend.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _service;

        public AuthController(IAuthService service) => _service = service;

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] DTOSignIn req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _service.SignIn(req);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] DTOSignUp req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _service.SignUp(req);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }

        [HttpPut("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] DTOForgotPassword req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _service.ForgotPassword(req);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }
    }
}
