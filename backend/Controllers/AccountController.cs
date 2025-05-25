using backend.Models;
using backend.Services;
using backend.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;

namespace backend.Controllers
{
    [Route("odata/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ODataController
    {
        private IAccountService _service;

        public AccountController(IAccountService service) => _service = service;

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Account>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();

            var res = await _service.GetOne(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPut("ban")]
        public async Task<IActionResult> BanAccount([FromBody] DTOBanAccount req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _service.BanAccount(req);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }

        [HttpPut("active/{id}")]
        public async Task<IActionResult> ActiveAccount([FromRoute] string id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();

            var res = await _service.ActiveAccount(id);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();

            var res = await _service.DeletedAccount(id);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }
    }
}
