using DevilCatBackend.Data;
using DevilCatBackend.Models;
using DevilCatBackend.Shared.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace DevilCatBackend.Services
{
    public interface IAccountService
    {
        IQueryable<Account> Get();
        Task<Account?> Get(string id);
        Task<DTORespone> Create(Account data);
        Task<DTORespone> Update(Account data);
        Task<DTORespone> Delete(string id);
        Task<DTORespone> DeleteMany(ICollection<string> ids);
    }

    public class AccountService : IAccountService
    {
        private UserDbContext _context;
        public AccountService(UserDbContext context) => _context = context;

        public async Task<DTORespone> Create(Account data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                string? temp = "";
                if (!data.FacebookeID.IsNullOrEmpty())
                {
                    temp = "Facebook";
                }
                else if (!data.GoogleID.IsNullOrEmpty())
                {
                    temp = "Google";

                }

                var account = new Account
                {
                    ID = Guid.NewGuid().ToString("N"),
                    UserName = data.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Avatar = data.Avatar,
                    IsBanned = data.IsBanned,
                    Reason = data.Reason,
                    AccountType = temp ?? "Local",
                    Role = data.Role,
                    FacebookeID = data.FacebookeID,
                    GoogleID = data.GoogleID,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = $"_id: {account.ID}", StatusCode = 201 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<DTORespone> Delete(string id)
        {
            if (id.IsNullOrEmpty())
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var account = await Get(id);
                if (account == null)
                    return new DTORespone { Message = "Account does not exist.", StatusCode = 404 };
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                return new DTORespone { StatusCode = 204 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<DTORespone> DeleteMany(ICollection<string> ids)
        {
            if (ids.IsNullOrEmpty())
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                List<Account> list = new List<Account>();
                foreach (string id in ids)
                {
                    var account = await Get(id);
                    list.Add(account!);
                }
                _context.Accounts.RemoveRange(list);
                await _context.SaveChangesAsync();
                return new DTORespone { StatusCode = 204 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public IQueryable<Account> Get()
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

        public async Task<Account?> Get(string id)
        {
            try
            {
                return await _context.Accounts.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DTORespone> Update(Account data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var account = await Get(data.ID);
                if (account == null)
                    return new DTORespone { Message = "Account does not exist.", StatusCode = 404 };

                data.UpdateAt = DateTime.Now;
                _context.Entry(account).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = $"_id: {data.ID}", StatusCode = 200 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }
    }
}
