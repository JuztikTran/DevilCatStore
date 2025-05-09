using DTOs;
using DTOs.Auth;
using DTOs.User;
using Microsoft.EntityFrameworkCore;
using StoreService.Data;
using StoreService.IServices.Auth;
using StoreService.IServices.User;
using StoreService.IServices.Utils;
using StoreService.Models.User;

namespace StoreService.Services.Auth
{
    public class AuthService : IAuthService
    {
        private UserDBContext userDBContext;
        private ITokenService _tokenService;
        private IUserService userService;

        public AuthService(UserDBContext context, ITokenService tokenService, IUserService userService)
        {
            this.userDBContext = context;
            this._tokenService = tokenService;
            this.userService = userService;
        }

        public Task<DTORespone> DeleteAccount(string accountID)
        {
            throw new NotImplementedException();
        }

        public async Task<DTORespone> ForgotPassword(DTOForgotPassword request)
        {
            if (request == null)
            {
                return new DTORespone { IsSuccess = false, Message = "Invalid data request." };
            }

            try
            {
                var account = await this.userDBContext.Accounts
                                        .FirstOrDefaultAsync(a => a.UserName.Equals(request.UserName));

                if (account == null) return new DTORespone { IsSuccess = false, Message = "Account does not exit" };

                account.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                account.UpdateAt = DateTime.Now;

                userDBContext.Accounts.Update(account);
                await this.userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Update password success!" };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }

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
                    AccountType = request.AccountType
                };

                userDBContext.Accounts.Add(acc);
                await userDBContext.SaveChangesAsync();

                var profile = new DTOProfile
                {
                    AccountID = acc.ID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    AvatarURI = request.AvatarURI
                };

                var rs = await userService.CreateProfile(profile);

                if (!rs.IsSuccess) throw new Exception(rs.Message);

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
