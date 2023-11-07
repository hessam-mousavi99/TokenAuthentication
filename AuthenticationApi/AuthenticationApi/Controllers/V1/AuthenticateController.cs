using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Services.Authenticate;
using AuthenticationApi.Services.User;
using AuthenticationApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly EncryptionUtility _encryptionUtility;
        private readonly TokenUtility _tokenUtility;

        public AuthenticateController(IAuthenticateService authenticateService, IUserService userService, IConfiguration configuration, EncryptionUtility encryptionUtility, TokenUtility tokenUtility)
        {
            _authenticateService = authenticateService;
            _userService = userService;
            _configuration = configuration;
            _encryptionUtility = encryptionUtility;
            _tokenUtility = tokenUtility;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginVM login)
        {
            var user = await _userService.GetUserByEmailAsync(login.Email);
            if (user == null)
            {
                return BadRequest("Invalid Email Address");
            }
            var hashPass = _encryptionUtility.HashSHA256(login.Password);
            if (user.Password != hashPass)
            {
                return BadRequest("Invalid Password");
            }
            var token = _tokenUtility.TokenGenerator(user.Id);
            var refreshToken = Guid.NewGuid();
            var info = new AuthenticateVM()
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = user.Email,
                Token = token,
                RefreshToken = refreshToken.ToString()
            };
            var userRefreshToken = await _authenticateService.GetTokenByUserIdAsync(user.Id);
            var userToken = new UserTokenVM()
            {
                UserId = user.Id,
                GenerateDate = DateTime.Now,
                RefreshToken = refreshToken.ToString(),
                IsValid = true
            };
            if (userRefreshToken == null)
            {
                await _authenticateService.InsertTokenAsync(userToken);
            }
            else
            {
                await _authenticateService.UpdateTokenAsync(userToken);
            }
            return Ok(info);
        }
    }
}
