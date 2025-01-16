using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.Login
{
    public class LoginCommand  : ICommand<string>{
        public string Username {get;set;}
        public string Password {get;set;}

    }
}