using Application.Auth.Commands.Login;
using Application.Auth.Commands.Registro;
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

        [HttpPost("registrarse")]
        public async Task<string> Registrarse([FromBody] RegistrarseRequest request){
           return await this._sender.Send(new RegistroCommand(){
            Username = request.Username,
            Password = request.Password
           });
        }
    }

    public class RegistrarseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}