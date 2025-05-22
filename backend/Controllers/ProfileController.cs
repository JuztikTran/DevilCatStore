using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ODataController
    {
        private IProfileService _service;
        public ProfileController(IProfileService service) => _service = service;

        [HttpGet("get/{id}")]
        public IActionResult GetOne(string id)
        {
            return Ok(new { id });
        }
    }
}
