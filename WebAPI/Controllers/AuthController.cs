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
        public async Task<dynamic> Login(string username, string password){
           return await this._sender.Send(new LoginCommand(){
            Username = username,
            Password = password
           });
        }
    }
}