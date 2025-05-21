using backend.Data;
using backend.Models;
using backend.Shared.DTOs;
using backend.Shared.Utils;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<DTORespone> SignIn(Account data);
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

        public async Task<DTORespone> SignIn(Account data)
        {
            try
            {
                if (data == null)
                    return new DTORespone { StatusCode = 400, Message = "Invalid data request." };

                var account = await _context.Accounts.FindAsync(data.UserName);
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };

                if (!BCrypt.Net.BCrypt.Verify(data.Password, account.Password))
                    return new DTORespone { StatusCode = 400, Message = "Incorrect username or password." };

                var token = _tokenService.RegistToken(data);

                return new DTORespone { StatusCode = 200, Message = token.ToString() };
            }
            catch (Exception e)
            {
                return new DTORespone { StatusCode = 500, Message = e.Message };
            }
        }
    }
}
