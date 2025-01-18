using Application.Core.Abstractions.Messaging;

namespace Application.Baneos.Commands.DesbanearUsuario
{
    public class DesbanearUsuarioCommand : ICommand
    {
        public Guid Usuario  { get; set; }
    }
}