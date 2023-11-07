using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.Utils
{
    public class TokenUtility
    {
        private readonly IConfiguration _configuration;

        public TokenUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string TokenGenerator(Guid userId)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenKey"));
            var tokenTimeOut = _configuration.GetValue<int>("TokenTimeOut");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",userId.ToString())     
                }),
                Expires=DateTime.UtcNow.AddMinutes(tokenTimeOut),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
