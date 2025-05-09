using DTOs;
using DTOs.Auth;

namespace StoreService.IServices.Auth
{
    public interface IAuthService
    {
        Task<DTORespone> SignIn(DTOSignIn request);
        Task<DTORespone> SignUp(DTOSignUp request);
        Task<DTORespone> ForgotPassword(DTOForgotPassword request);
        Task<DTORespone> DeleteAccount(string accountID);
    }
}
