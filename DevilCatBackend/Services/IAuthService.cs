using BCrypt.Net;
using DevilCatBackend.Data;
using DevilCatBackend.Models;
using DevilCatBackend.Shared.DTOs;
using DevilCatBackend.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DevilCatBackend.Services
{
    public interface IAuthService
    {
        Task<DTORespone> SignIn(DTOSignIn data);
        Task<DTORespone> SignUp(DTOSignUp data);
        Task<DTORespone> ForgotPassword(DTOForgotPass data);
    }

    public class AuthService : IAuthService
    {
        private UserDbContext _context;
        private ITokenService _tokenService;
        public AuthService(UserDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<DTORespone> ForgotPassword(DTOForgotPass data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var account = await _context.Accounts
                    .Where(a => a.Email.ToLower() == data.Email!.ToLower()).FirstOrDefaultAsync();
                if (account == null)
                    return new DTORespone { Message = "Does not exist account have this email.", StatusCode = 404 };
                account.Password = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
                return new DTORespone { StatusCode = 200, Message = "Change password succcessful." };
            }
            catch (Exception e)
            {
                return new DTORespone
                {
                    Message = e.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<DTORespone> SignIn(DTOSignIn data)
        {
            if (data == null)
                return new DTORespone { StatusCode = 400, Message = "Invalid data request." };
            try
            {
                var account = await _context.Accounts
                    .Where(a => a.UserName.ToLower() == data.UserName!.ToLower())
                    .FirstOrDefaultAsync();
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };
                if (account.IsBanned)
                    return new DTORespone { StatusCode = 404, Message = account.Reason };
                if (!BCrypt.Net.BCrypt.Verify(data.Password, account.Password))
                    return new DTORespone { StatusCode = 404, Message = "Incorrect username or password." };
                var token = _tokenService.GetToken(account);
                return new DTORespone { StatusCode = 200, Message = token.ToString() };
            }
            catch (Exception e)
            {
                return new DTORespone { StatusCode = 500, Message = e.Message };
            }
        }

        public async Task<DTORespone> SignUp(DTOSignUp data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                string temp = "";
                if (data.GoogleID.IsNullOrEmpty())
                {
                    temp = "Google";
                }
                else if (data.FacebookeID.IsNullOrEmpty())
                {
                    temp = "Facebook";
                }
                var account = new Account
                {
                    ID = Guid.NewGuid().ToString("N"),
                    UserName = data.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    AccountType = temp ?? "Local",
                    IsBanned = false,
                    Reason = "",
                    Avatar = "",
                    Role = "User",
                    GoogleID = data.GoogleID,
                    FacebookeID = data.FacebookeID,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = $"_id: {account.ID}", StatusCode = 201 };
            }
            catch (Exception e)
            {
                return new DTORespone
                {
                    Message = e.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
