using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Services.User;
using AuthenticationApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpPost]
        public async Task<IActionResult> Post(UserVM user)
        {
            var HashPass = _encryptionUtility.HashSHA256(user.Password);
            var model = new UserVM
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = HashPass,
                IsActive = true
            };
            await _userService.InsertUserAsync(model);
            return Ok(model);
        }
        [HttpPut("update-user/{email}")]
        public async Task<IActionResult> Put(string email,UpdateUserVM user)
        {
            user.Password=_encryptionUtility.HashSHA256(user.Password);
            var model=await _userService.GetUserByEmailAsync(email);
            await _userService.UpdateUserAsync(model.Id, user);
            return Ok(user);
        }
        [HttpDelete("delete-user/{email}")]
        public async Task<IActionResult> delete(string email)
        {
            var model = await _userService.GetUserByEmailAsync(email);
            await _userService.DeleteUserAsync(model.Id);
            return Ok();
        }

    }
}
