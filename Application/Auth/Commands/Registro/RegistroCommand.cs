using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.Registro
{
    public class RegistroCommand  : ICommand<string>{
        public string Username {get;set;}
        public string Password {get;set;}
    }
}