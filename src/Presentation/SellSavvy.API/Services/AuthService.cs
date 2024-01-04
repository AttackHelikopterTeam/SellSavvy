using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SellSavvy.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SellSavvy.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Person> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<Person> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> RegisterUser(Person user)
        {
            var person = new Person
            {
                UserName = user.UserName,
                Email = user.UserName
            };

            var result = await _userManager.CreateAsync(person, person.PasswordHash);
            return result.Succeeded;
        }

        public async Task<bool> Login(Person user)
        {
            var person = await _userManager.FindByEmailAsync(user.UserName);
            if (person is null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(person, user.PasswordHash);
        }

        public string GenerateTokenString(Person user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}