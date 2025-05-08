using StoreService.Data;
using StoreService.IServices.User;
using StoreService.Models.User;

namespace StoreService.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserDBContext userDBContext;

        public UserService(UserDBContext context)
        {
            this.userDBContext = context;
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
    }
}
