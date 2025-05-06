using DTOs;
using DTOs.Auth;
using StoreService.IServices.Auth;

namespace StoreService.Services.Auth
{
    public class AuthService : IAuthService
    {
        public Task<DTORespone> ForgotPassword(DTOForgotPassword request)
        {
            throw new NotImplementedException();
        }

        public Task<DTORespone> SignIn(DTOSignIn request)
        {
            throw new NotImplementedException();
        }

        public Task<DTORespone> SignUp(DTOSignUp request)
        {
            throw new NotImplementedException();
        }
    }
}
