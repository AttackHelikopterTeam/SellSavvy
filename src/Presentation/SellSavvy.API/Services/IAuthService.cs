using SellSavvy.API.Models.LoginModels;
using SellSavvy.Domain.Identity;

namespace SellSavvy.API.Service
{
        public interface IAuthService
        {
            string GenerateTokenString(LoginUser user);
            Task<bool> Login(LoginUser user);
            Task<bool> RegisterUser(LoginUser user);
        }

    }
