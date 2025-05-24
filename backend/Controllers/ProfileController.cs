using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private IProfileService _service;
        public ProfileController(IProfileService service) => _service = service;

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var res = await _service.GetOne(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Profile req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.Create(req);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Profile req)
        {
            if (!ModelState.IsValid || id.IsNullOrEmpty() || id != req.ID)
                return BadRequest(ModelState);
            var res = await _service.Update(req);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var res = await _service.Delete(id);
            return StatusCode(statusCode: res.StatusCode, value: res.Message);
        }
    }
}
