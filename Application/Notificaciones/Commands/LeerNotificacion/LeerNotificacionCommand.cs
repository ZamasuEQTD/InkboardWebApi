using Application.Core.Abstractions.Messaging;

namespace Application.Notificaciones.Commands.LeerNotificacion
{
    public class LeerNotificacionCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}