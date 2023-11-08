using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Services.User;
using AuthenticationApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers.V1
{

    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors(CorsPolicyKeys.Policy_A)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly EncryptionUtility _encryptionUtility;

        public UserController(IUserService userService, EncryptionUtility encryptionUtility)
        {
            _userService = userService;
            _encryptionUtility = encryptionUtility;
        }
        [HttpGet("ByEmail")]
        public async Task<IActionResult> GetByEmail(string Email)
        {
            var user = await _userService.GetUserByEmailAsync(Email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("ById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize]
        [HttpPut("update-user/{email}")]
        public async Task<IActionResult> Put(string email,UpdateUserVM user)
        {
            user.Password=_encryptionUtility.HashSHA256(user.Password);
            var model=await _userService.GetUserByEmailAsync(email);
            await _userService.UpdateUserAsync(model.Id, user);
            return Ok(user);
        }
        [Authorize]
        [HttpDelete("delete-user/{email}")]
        public async Task<IActionResult> delete(string email)
        {
            var model = await _userService.GetUserByEmailAsync(email);
            await _userService.DeleteUserAsync(model.Id);
            return Ok();
        }

    }
}
