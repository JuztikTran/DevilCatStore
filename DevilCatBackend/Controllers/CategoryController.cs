using DevilCatBackend.Models;
using DevilCatBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevilCatBackend.Controllers
{
    [Route("odata/category")]
    [ApiController]
    public class CategoryController : ODataController
    {
        private ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        [AllowAnonymous]
        public ActionResult<ICollection<Category>> Get()
        {
            var list = _service.Get();
            return Ok(list);
        }
    }
}
