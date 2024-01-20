using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellSavvy.API.Models.LoginModels;
using SellSavvy.API.Services;

using SellSavvy.API.Service;

using SellSavvy.Domain.Identity;

namespace SellSavvy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private RegisterService _registerService;  //registercount
        private RequestCountService _requestCountService; //requestCount


        public AuthController(IAuthService authService, RegisterService registerService, RequestCountService requestCountService)
        {
            _registerService = registerService; //registercount 
            _authService = authService;
            _requestCountService = requestCountService; //requestCount
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUser user)
        {
            if (await _authService.RegisterUser(user))
            {
                _requestCountService.RequestCount++; //requestCount
                _registerService.RegistrationCount++;
                return Ok("Successfuly done");
            }
            return BadRequest("Something went worng");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _authService.Login(user))
            {
                _requestCountService.RequestCount++; //requestCount

                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();
        }


        [HttpGet ("GetCountOfAccount")] //registercount
        public IActionResult GetCountOfAccount() //registercount
        {

            return Ok(_registerService.RegistrationCount); //registercount
        }



        [HttpGet("GetCountOfRequest")] //requestCount
        public IActionResult GetCountOfRequest() //requestCount
        {

            _requestCountService.RequestCount++; //requestCount
            return Ok(_requestCountService.RequestCount); //requestCount

        }

    }
}