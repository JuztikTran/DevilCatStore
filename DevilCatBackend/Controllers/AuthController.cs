using DevilCatBackend.Services;
using DevilCatBackend.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevilCatBackend.Controllers
{
    [Route("odata/auth")]
    //[ApiController]
    public class AuthController : ODataController
    {
        private IAuthService _service;
        public AuthController(IAuthService service) => _service = service;

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<DTORespone>> SignIn([FromBody] DTOSignIn request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var respone = await _service.SignIn(request);
            return StatusCode(respone.StatusCode, respone.Message);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<DTORespone>> SignUp([FromBody] DTOSignUp request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var respone = await _service.SignUp(request);
            return StatusCode(respone.StatusCode, respone.Message);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<DTORespone>> ForgotPassword([FromBody] DTOForgotPass request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var respone = await _service.ForgotPassword(request);
            return StatusCode(respone.StatusCode, respone.Message);
        }
    }
}
