using Application.Auth.Commands.Login;
using Application.Auth.Commands.Registro;
using Domain.Core;
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
        public async Task<IResult> Login([FromBody] LoginRequest request){
            var result = await this._sender.Send(new LoginCommand(){
                Username = request.Username,
                Password = request.Password
            });

           return result.ToResult();
        }

        [HttpPost("registrarse")]
        public async Task<IResult> Registrarse([FromBody] RegistrarseRequest request){            
            var result =  await this._sender.Send(new RegistroCommand(){
                Username = request.Username,
                Password = request.Password
            });

           return result.ToResult();
        }
    }


    static public class ResultExtensions
    {
        static public IResult ToResult(this Result result){
            if (result.IsSuccess)
            {
                return Results.NoContent();
            }
            return result.Error.ToProblemDetails();
        }

        static public IResult ToResult<T>(this Result<T> result){

       
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
   

            return result.Error.ToProblemDetails();
        }


        static private IResult ToProblemDetails( this Error error){
            return Results.BadRequest(new ProblemDetails(){
                Title =  error.Code,
                Detail = error.Descripcion
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