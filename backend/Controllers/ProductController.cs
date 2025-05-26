using backend.Models;
using backend.Services;
using backend.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("odata/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ODataController
    {
        private IProductService _service;
        public ProductController(IProductService service) => _service = service;

        [HttpGet]
        [EnableQuery]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var res = await _service.GetOne(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DTOProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.Create(product);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Product req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.Update(req);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DTODelete req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.DeleteMany(req);
            return StatusCode(res.StatusCode, res.Message);
        }
    }
}
