using DTOs;
using StoreService.Models.User;

namespace StoreService.IServices.User
{
    public interface IUserService
    {
        Task<Profile> GetProfile(string accountID);
        Task<DTORespone> CreateProfile(Profile profile);
        Task<DTORespone> UpdateProfile(Profile profile);
        Task<DTORespone> DeleteProfile(string accountID);
        IQueryable<Address> GetAddresses();
        Task<Address?> GetAddress(string addressID);
        Task<DTORespone> CreateAddress(Address address);
        Task<DTORespone> UpdateAddresses(Address address);
        Task<DTORespone> DeleteAddresses(string addressID);
    }
}
