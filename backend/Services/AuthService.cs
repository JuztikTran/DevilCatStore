using backend.Data;
using backend.Models;
using backend.Shared.DTOs;
using backend.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<DTORespone> SignIn(DTOSignIn data);
        Task<DTORespone> SignUp(DTOSignUp data);
        Task<DTORespone> ForgotPassword(DTOForgotPassword data);
    }

    public class AuthService : IAuthService
    {
        private UserDbContext _context;
        private ItokenService _tokenService;

        public AuthService(UserDbContext context, ItokenService service)
        {
            _context = context;
            _tokenService = service;
        }

        public async Task<DTORespone> ForgotPassword(DTOForgotPassword data)
        {
            try
            {
                if (data == null)
                    return new DTORespone { StatusCode = 400, Message = "Invalid data request." };

                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName.Equals(data.UserName.ToLower()));
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };

                account.Password = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);

                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return new DTORespone { Message = "Ok", StatusCode = 200 };
            }
            catch (Exception e)
            {
                return new DTORespone { StatusCode = 500, Message = e.Message };
            }
        }

        public async Task<DTORespone> SignIn(DTOSignIn data)
        {
            try
            {
                if (data == null)
                    return new DTORespone { StatusCode = 400, Message = "Invalid data request." };

                var account = await _context.Accounts.Where(a => a.UserName.ToLower() == data.UserName.ToLower())
                    .FirstOrDefaultAsync();
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };

                if (account.IsBanned)
                    return new DTORespone { StatusCode = 400, Message = account.Reason };

                if (!BCrypt.Net.BCrypt.Verify(data.Password, account.Password))
                    return new DTORespone { StatusCode = 400, Message = "Incorrect username or password." };

                var token = _tokenService.RegistToken(account);

                return new DTORespone { StatusCode = 200, Message = token.ToString() };
            }
            catch (Exception e)
            {
                return new DTORespone { StatusCode = 500, Message = e.Message };
            }
        }

        public async Task<DTORespone> SignUp(DTOSignUp data)
        {
            try
            {
                if (data == null)
                    return new DTORespone { StatusCode = 400, Message = "Invalid data request." };

                Account account;
                string hash = BCrypt.Net.BCrypt.HashPassword(data.Password);

                if (data.GoogleId.IsNullOrEmpty() || data.FacebookId.IsNullOrEmpty())
                {
                    account = new Account
                    {
                        ID = new Guid().ToString("N"),
                        UserName = data.UserName.ToLower(),
                        Password = hash,
                        Email = data.Email,
                        Role = data.Role,
                        AccountType = data.AccountType,
                        IsBanned = data.IsBanned,
                        IsActive = false,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    };
                }
                else
                {
                    account = new Account
                    {
                        ID = data.GoogleId ?? data.FacebookId!,
                        UserName = data.UserName.ToLower(),
                        Password = hash,
                        Email = data.Email,
                        Role = data.GoogleId.IsNullOrEmpty() ? "FACEBOOK" : "GOOGLE",
                        AccountType = data.AccountType,
                        IsBanned = data.IsBanned,
                        IsActive = !data.GoogleId.IsNullOrEmpty(),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    };
                }

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return new DTORespone { StatusCode = 201, Message = "Sign up successful." };
            }
            catch (Exception e)
            {
                return new DTORespone { StatusCode = 500, Message = e.Message };
            }
        }
    }
}
