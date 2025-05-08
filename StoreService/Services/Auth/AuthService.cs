using DTOs;
using DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using StoreService.Data;
using StoreService.IServices.Auth;
using StoreService.IServices.Utils;
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
        private ITokenService _tokenService;

        public AuthService(UserDBContext context, IConfiguration configuration, ITokenService tokenService)
        {
            this.userDBContext = context;
            this._configuration = configuration;
            this._tokenService = tokenService;
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
                    Message = _tokenService.Authenticate(account).ToString()!
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

    }

}
