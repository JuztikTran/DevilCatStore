using DTOs.Auth;
using Microsoft.IdentityModel.Tokens;
using StoreService.Data;
using StoreService.IServices.Utils;
using StoreService.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreService.Services.Utils
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _userDBContext;

        public TokenService(UserDBContext context, IConfiguration configuration)
        {
            this._userDBContext = context;
            this._configuration = configuration;
        }

        public DTOToken Authenticate(Account account)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpireTimestamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, account.UserName),
                }),
                Expires = tokenExpireTimestamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new DTOToken
            {
                UserName = account.UserName,
                Token = accessToken,
                ExpireIn = (int)tokenExpireTimestamp.Subtract(DateTime.UtcNow).TotalSeconds,
            };
        }
    }
}
