using DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using StoreService.IServices.User;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreService.Controllers
{
    [Route("api/address")]
    [ApiController]
    [Authorize]
    public class AddressController : ODataController
    {
        private IUserService _userService;

        public AddressController(IUserService userService) => _userService = userService;

        // GET: api/<AddressController>
        [HttpGet("get")]
        public IActionResult Get()
        {
            try
            {
                var respone = _userService.GetAddresses();
                return Ok(respone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<AddressController>/<addressId>
        [HttpGet("get/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var respone = _userService.GetAddress(id);
                return respone != null ? Ok(respone) : NotFound("Not found address with this ID.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTOAddress request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data request");
            }
            try
            {
                var respone = await _userService.CreateAddress(request);
                return respone.IsSuccess ? Ok(respone) : throw new Exception(respone.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<AddressController>/<addressID>
        [HttpPut("update/{aid}")]
        public async Task<IActionResult> Put(string aid, [FromBody] DTOAddressUpdate request)
        {
            if (!ModelState.IsValid || aid.IsNullOrEmpty() || aid.Equals(request.ID))
            {
                return BadRequest("Invalid data request.");
            }
            try
            {
                var respone = await _userService.UpdateAddresses(request);
                return respone.IsSuccess ? Ok(new { id = aid, message = respone.Message }) : throw new Exception(respone.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{aid}")]
        public async Task<IActionResult> Delete(string aid)
        {
            try
            {
                var respone = await _userService.DeleteAddresses(aid);
                return respone.IsSuccess ? Ok(new { id = aid, message = respone.Message }) : throw new Exception(respone.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
