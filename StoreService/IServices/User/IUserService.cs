using StoreService.Models.User;

namespace StoreService.IServices.User
{
    public interface IUserService
    {
        IQueryable<Address> GetAddresses();
    }
}
