using DTOs.Auth;
using StoreService.Models.User;

namespace StoreService.IServices.Utils
{
    public interface ITokenService
    {
        DTOToken Authenticate(Account account);
    }
}
