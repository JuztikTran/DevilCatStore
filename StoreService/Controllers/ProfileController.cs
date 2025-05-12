using DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using StoreService.IServices.User;
using StoreService.Models.User;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreService.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ODataController
    {
        private IUserService userService;

        public ProfileController(IUserService userService) => this.userService = userService;

        // GET: api/<ProfileController>
        [HttpGet("all")]
        [EnableQuery]
        public IActionResult Get()
        {
            var respone = userService.GetProfiles();
            return Ok(respone);
        }

        // GET api/<ProfileController>/<accountID>
        [HttpGet("get/{aid}")]
        public async Task<IActionResult> Get(string aid)
        {
            try
            {
                var response = await this.userService.GetProfile(aid);
                return response == null ? NotFound($"Not found profile with this account ID") : Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<ProfileController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTOProfile request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data request");
            }
            try
            {
                var respone = await this.userService.CreateProfile(request);
                return respone.IsSuccess ? NoContent() : throw new Exception(respone.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ProfileController>/<profileId>
        [HttpPut("update/{pid}")]
        public async Task<IActionResult> Put(string pid, [FromBody] DTOProfileUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data request.");
            }
            try
            {
                if (!pid.Equals(request.ID) || pid.IsNullOrEmpty())
                    throw new Exception("Profile id is empty or not match.");

                var respone = await this.userService.UpdateProfile(request);
                return respone.IsSuccess ? Ok(new { id = request.ID, message = respone.Message }) : throw new Exception(respone.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<ProfileController>/<profileId>
        [HttpDelete("delete/{pid}")]
        public async Task<IActionResult> Delete(string pid)
        {
            try
            {
                var respone = await userService.DeleteProfile(pid);
                return respone.IsSuccess ? Ok(new {id = pid, message = respone.Message}) : throw new Exception(respone.Message) ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
