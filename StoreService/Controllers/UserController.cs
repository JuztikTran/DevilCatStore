using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using StoreService.IServices.User;
using StoreService.Models.User;

namespace StoreService.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ODataController
    {
        private IUserService userService;

        public UserController(IUserService service)
        {
            this.userService = service;
        }

        [EnableQuery]
        [HttpGet("address/all")]
        public IActionResult GetAllAddress()
        {
            var respone = userService.GetAddresses();
            return Ok(respone);
        }
    }
}
