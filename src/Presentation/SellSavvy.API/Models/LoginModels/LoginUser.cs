using SellSavvy.Domain.Enum;

namespace SellSavvy.API.Models.LoginModels
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public Gender  Gender { get; set; }
        public string? Password { get; set; }

    }
}
