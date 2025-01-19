
using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.CambiarNotificaciones {
    public class CambiarNotificacionesHiloCommand :ICommand
    {
        public Guid HiloId { get; set; }
    }
}