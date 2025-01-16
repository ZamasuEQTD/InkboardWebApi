using Application.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("login")]
        public async Task<string> Login([FromBody] LoginRequest request){
           return await this._sender.Send(new LoginCommand(){
            Username = request.Username,
            Password = request.Password
           });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}