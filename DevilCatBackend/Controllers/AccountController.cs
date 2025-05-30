using DevilCatBackend.Models;
using DevilCatBackend.Services;
using DevilCatBackend.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;

namespace DevilCatBackend.Controllers
{
    [Route("odata/account")]
    //[ApiController]
    public class AccountController : ODataController
    {
        private IAccountService _service;
        public AccountController(IAccountService service) => _service = service;

        [HttpGet]
        [EnableQuery]
        public ActionResult<ICollection<Account>> Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account?>> Get([FromRoute] string id)
        {
            var respone = await _service.Get(id);
            return respone == null ? NotFound() : Ok(respone);
        }

        [HttpPost]
        public async Task<ActionResult<DTORespone>> Create([FromBody] Account request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var respone = await _service.Create(request);
            return StatusCode(respone.StatusCode, respone.Message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DTORespone>> Update([FromRoute] string id, [FromBody] Account request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id.IsNullOrEmpty() || id != request.ID)
                return BadRequest("Invalid id request or not match.");
            var respone = await _service.Update(request);
            return StatusCode(respone.StatusCode, respone.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DTORespone>> Delete([FromRoute] string id)
        {
            var respone = await _service.Delete(id);
            return StatusCode(respone.StatusCode, respone.Message);
        }

        [HttpDelete]
        public async Task<ActionResult<DTORespone>> DeleteMany([FromBody] ICollection<string> request)
        {
            if (request.IsNullOrEmpty())
                return BadRequest("An empty list delete request.");
            var respone = await _service.DeleteMany(request);
            return StatusCode(respone.StatusCode, respone.Message);
        }
    }
}
