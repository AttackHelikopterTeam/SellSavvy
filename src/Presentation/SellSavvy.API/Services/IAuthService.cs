using SellSavvy.Domain.Identity;

namespace SellSavvy.API.Service
{
    public interface IAuthService
    {
        string GenerateTokenString(Person user);
        Task<bool> Login(Person user);
        Task<bool> RegisterUser(Person user);

    }
}