using DTOs;
using DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using StoreService.Data;
using StoreService.IServices.Auth;
using StoreService.Models;
using StoreService.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreService.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserDBContext userDBContext;
        private readonly IConfiguration _configuration;


        public AuthService(UserDBContext context, IConfiguration configuration)
        {
            this.userDBContext = context;
            this._configuration = configuration;
        }
        public Task<DTORespone> ForgotPassword(DTOForgotPassword request)
        {
            throw new NotImplementedException();
        }

        public async Task<DTORespone> SignIn(DTOSignIn request)
        {
            if (request == null)
                return new DTORespone
                {
                    IsSuccess = false,
                    Message = "Invalid data request."
                };

            try
            {
                var account = await userDBContext.Accounts
                .Where(a => a.UserName.Equals(request.UserName))
                .FirstOrDefaultAsync();

                if (account == null)
                    return new DTORespone
                    {
                        IsSuccess = false,
                        Message = "Account does not exist."
                    };

                if (!BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
                    return new DTORespone
                    {
                        IsSuccess = false,
                        Message = "Username or password is incorrect."
                    };

                return new DTORespone
                {
                    IsSuccess = true,
                    Message = CreateToken(account.UserName).ToString()!
                };
            }
            catch (Exception e)
            {
                return new DTORespone
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }            
        }

        public async Task<DTORespone> SignUp(DTOSignUp request)
        {
            if (request == null)
                return new DTORespone
                {
                    IsSuccess = false,
                    Message = "Invalid data request."
                };

            try
            {
                var account = await userDBContext.Accounts
                .Where(a => a.UserName.Equals(request.UserName))
                .FirstOrDefaultAsync();

                if (account != null)
                    return new DTORespone
                    {
                        IsSuccess = false,
                        Message = "Account does not exist."
                    };

                var acc = new Account
                {
                    UserName = request.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Email = request.Email,
                    Role = (Models.Role)request.Role,
                    IsActive = request.IsActive,
                    IsBan = request.IsBan,
                    AccountType = request.AccountType,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                userDBContext.Accounts.Add(acc);
                await userDBContext.SaveChangesAsync();

                var profile = new Profile
                {
                    AccountID = acc.ID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = (Models.Gender)request.Gender,
                    AvatarURI = request.AvatarURI,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                userDBContext.Profiles.Add(profile);
                await userDBContext.SaveChangesAsync();

                return new DTORespone
                {
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch (Exception e)
            {
                return new DTORespone
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
            
        }

        private DTOToken CreateToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(3); // Token có hiệu lực 3 ngày

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new DTOToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                DateOfExpire = expiration
            };
        }

    }

}
