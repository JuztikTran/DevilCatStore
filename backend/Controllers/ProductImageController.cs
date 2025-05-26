using backend.Data;
using backend.Models;
using backend.Services;
using backend.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("odata/product-image")]
    [ApiController]
    [Authorize]
    public class ProductImageController : ODataController
    {
        private IProductImageService _service;
        public ProductImageController(IProductImageService service) => _service = service;

        [HttpGet]
        [AllowAnonymous]
        [EnableQuery]
        public ActionResult<IEnumerable<ProductImages>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ICollection<DTOProductImage> req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await _service.Create(req);
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] ProductImages req)
        {
            if (!ModelState.IsValid || id.IsNullOrEmpty() || id.ToLower() != req.ID.ToLower())
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
