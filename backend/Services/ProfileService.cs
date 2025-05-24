using backend.Data;
using backend.Models;
using backend.Shared.DTOs;

namespace backend.Services
{
    public interface IProfileService
    {
        Task<Profile?> GetOne(string id);
        Task<DTORespone> Create(Profile data);
        Task<DTORespone> Update(Profile data);
        Task<DTORespone> Delete(string id);
    }

    public class ProfileService : IProfileService
    {
        private UserDbContext _context;

        public ProfileService(UserDbContext context) => _context = context;

        public async Task<DTORespone> Create(Profile data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                _context.Profiles.Add(data);
                await _context.SaveChangesAsync();

                return new DTORespone { StatusCode = 201, Message = $"{data.ID}" };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<DTORespone> Delete(string id)
        {
            try
            {
                var profile = await GetOne(id);
                if (profile == null)
                    return new DTORespone { StatusCode = 404, Message = "Profile information does not exist." };

                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = "Deleted", StatusCode = 204 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<Profile?> GetOne(string id)
        {
            try
            {
                return await _context.Profiles.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DTORespone> Update(Profile data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var profile = await GetOne(data.ID);
                if (profile == null)
                    return new DTORespone { StatusCode = 404, Message = "Profile does not exist." };

                profile.FirstName = data.FirstName;
                profile.LastName = data.LastName;
                profile.Gender = data.Gender;
                profile.Avatar = data.Avatar;
                profile.UpdateAt = DateTime.Now;

                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();

                return new DTORespone { StatusCode = 200, Message = $"{profile.ID}" };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }
    }
}
