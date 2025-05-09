using DTOs;
using DTOs.User;
using StoreService.Models.User;

namespace StoreService.IServices.User
{
    public interface IUserService
    {
        Task<DTORespone> ChangePassword(DTOChangePassword request);
        IQueryable<Profile> GetProfiles();
        Task<Profile?> GetProfile(string accountID);
        Task<DTORespone> CreateProfile(DTOProfile request);
        Task<DTORespone> UpdateProfile(DTOProfileUpdate request);
        Task<DTORespone> DeleteProfile(string accountID);
        IQueryable<Address> GetAddresses();
        Task<Address?> GetAddress(string request);
        Task<DTORespone> CreateAddress(DTOAddress request);
        Task<DTORespone> UpdateAddresses(DTOAddressUpdate request);
        Task<DTORespone> DeleteAddresses(string addressID);
    }
}
