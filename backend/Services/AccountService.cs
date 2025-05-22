using backend.Data;
using backend.Models;
using backend.Shared.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public interface IAccountService
    {
        IQueryable<Account> GetAll();
        Task<Account?> GetOne(string id);
        Task<DTORespone> ActiveAccount(string id);
        Task<DTORespone> BanAccount(DTOBanAccount data);
        Task<DTORespone> DeletedAccount(string id);
    }

    public class AccountService : IAccountService
    {
        private UserDbContext _context;

        public AccountService(UserDbContext context) => _context = context;

        public async Task<DTORespone> ActiveAccount(string id)
        {
            try
            {
                if (id.IsNullOrEmpty())
                    return new DTORespone { StatusCode = 400, Message = "An empty id." };

                var account = await GetOne(id);
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };

                account.IsActive = true;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return new DTORespone { StatusCode = 200, Message = "Actived" };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<DTORespone> BanAccount(DTOBanAccount data)
        {
            try
            {
                if (data == null)
                    return new DTORespone { StatusCode = 400, Message = "Invalid data request." };

                var account = await this.GetOne(data.ID);
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };

                account.IsBanned = true;
                account.Reason = data.Reason;

                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return new DTORespone { StatusCode = 200, Message = "Banned" };
            }
            catch (Exception e)
            {
                return new DTORespone { StatusCode = 500, Message = e.Message };
            }
        }

        public async Task<DTORespone> DeletedAccount(string id)
        {
            try
            {
                if (id.IsNullOrEmpty())
                    return new DTORespone { StatusCode = 400, Message = "An empty id." };

                var account = await this.GetOne(id);
                if (account == null)
                    return new DTORespone { StatusCode = 404, Message = "Account does not exist." };

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                return new DTORespone { StatusCode = 204, Message = "Deleted" };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IQueryable<Account> GetAll()
        {
            try
            {
                return _context.Accounts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account?> GetOne(string id)
        {
            try
            {
                if (id.IsNullOrEmpty())
                    return null;

                return await _context.Accounts.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
