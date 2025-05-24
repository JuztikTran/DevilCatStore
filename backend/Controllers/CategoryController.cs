using backend.Models;
using backend.Services;
using backend.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("odata/category")]
    [ApiController]
    public class CategoryController : ODataController
    {
        private ICategoryService _service;

        public CategoryController(ICategoryService service) => _service = service;

        [HttpGet("all")]
        [EnableQuery]
        public IQueryable GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var res = await _service.GetOne(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DTOCategory req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.Create(req);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Category req)
        {
            if (!ModelState.IsValid || id.IsNullOrEmpty() || id != req.ID)
                return BadRequest(ModelState);
            var res = await _service.Update(req);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DTODeleteCategory req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.DeleteMany(req);
            return StatusCode(res.StatusCode, res.Message);
        }
    }
}
