using DTOs;
using DTOs.User;
using Microsoft.EntityFrameworkCore;
using StoreService.Data;
using StoreService.IServices.User;
using StoreService.Models.User;

namespace StoreService.Services.User
{
    public class UserService : IUserService
    {
        private UserDBContext userDBContext;

        public UserService(UserDBContext context)
        {
            this.userDBContext = context;
        }

        public async Task<DTORespone> CreateAddress(DTOAddress request)
        {
            if (request == null)
            {
                return new DTORespone { IsSuccess = false, Message = "Invalid data request." };
            }

            try
            {
                var address = new Address
                {
                    ReceiverName = request.ReceiverName,
                    PhoneNumber = request.PhoneNumber,
                    Location = request.Location,
                    AccountId = request.AccountId,
                    IsDefault = request.IsDefault,
                    Note = request.Note,
                };
                userDBContext.Addresses.Add(address);
                await userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Create address success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<DTORespone> CreateProfile(DTOProfile request)
        {
            if (request == null)
            {
                return new DTORespone { IsSuccess = false, Message = "Invalid data request" };
            }

            try
            {
                var profile = new Profile
                {
                    AccountID = request.AccountID,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    AvatarURI = request.AvatarURI,
                    DateOfBirth = request.DateOfBirth,
                    Gender = (Models.Gender)request.Gender
                };
                userDBContext.Profiles.Add(profile);
                await userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = false, Message = "Create profile success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<DTORespone> DeleteAddresses(string addressID)
        {
            try
            {
                var address = await this.GetAddress(addressID);
                if (address == null) return new DTORespone { IsSuccess = false, Message = "Address does not exit." };

                userDBContext.Addresses.Remove(address);
                await userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Delete success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<DTORespone> DeleteProfile(string accountID)
        {
            try
            {
                var profile = await this.GetProfile(accountID);
                if (profile == null) throw new Exception("Profle does not exist.");

                userDBContext.Profiles.Remove(profile);
                await this.userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Delete profile success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<Address?> GetAddress(string addressID)
        {
            return await userDBContext.Addresses.FirstOrDefaultAsync(a => a.AccountId.Equals(addressID));
        }

        public IQueryable<Address> GetAddresses()
        {
            return userDBContext.Addresses
                .Select(a => new Address
                {
                    ID = a.ID,
                    Location = a.Location,
                    PhoneNumber = a.PhoneNumber,
                    ReceiverName = a.ReceiverName,
                    Account = a.Account,
                    AccountId = a.AccountId,
                    IsDefault = a.IsDefault,
                    Note = a.Note,
                    CreateAt = a.CreateAt,
                    UpdateAt = a.UpdateAt,
                });
        }

        public async Task<Profile?> GetProfile(string accountID)
        {
            return await this.userDBContext.Profiles.FirstOrDefaultAsync(p => p.AccountID.Equals(accountID));
        }

        public IQueryable<Profile> GetProfiles()
        {
            return this.userDBContext.Profiles
                .Select(p => new Profile
                {
                    ID = p.ID,
                    AccountID = p.AccountID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    AvatarURI = p.AvatarURI,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    CreateAt = p.CreateAt,
                    UpdateAt = p.UpdateAt,
                });
        }

        public async Task<DTORespone> UpdateAddresses(DTOAddressUpdate request)
        {
            if (request == null) return new DTORespone { IsSuccess = false, Message = "Invalid data request." };

            try
            {
                var address = await this.GetAddress(request.ID);

                if (address == null) throw new Exception("Address does not exist.");

                address.ReceiverName = request.ReceiverName;
                address.PhoneNumber = request.PhoneNumber;
                address.Location = request.Location;
                address.IsDefault = request.IsDefault;
                address.Note = request.Note;
                address.UpdateAt = DateTime.Now;

                userDBContext.Addresses.Update(address);
                await this.userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Update success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<DTORespone> ChangePassword(DTOChangePassword request)
        {
            if (request == null) return new DTORespone { IsSuccess = false, Message = "Invalid data request." };

            try
            {
                var account = await this.userDBContext.Accounts
                    .FirstOrDefaultAsync(a => a.UserName.Equals(request.UserName));

                if (account == null) throw new Exception("Account does not exist");

                if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, account.Password))
                    throw new Exception("Old password does not match.");

                account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                userDBContext.Accounts.Update(account);
                await this.userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Update password success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }

        public async Task<DTORespone> UpdateProfile(DTOProfileUpdate request)
        {
            if (request == null) return new DTORespone { IsSuccess = false, Message = "Invalid data request." };

            try
            {
                var profile = await this.GetProfile(request.ID);
                if (profile == null) throw new Exception("Profile does not exist.");

                profile.FirstName = request.FirstName;
                profile.LastName = request.LastName;
                profile.DateOfBirth = request.DateOfBirth;
                profile.AvatarURI = request.AvatarURI;
                profile.Gender = (Models.Gender)request.Gender;
                profile.UpdateAt = DateTime.Now;

                userDBContext.Profiles.Update(profile);
                await this.userDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Update profile success" };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }
        }
    }
}
