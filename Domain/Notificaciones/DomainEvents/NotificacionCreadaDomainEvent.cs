using Domain.Core;

namespace Domain.Notificaciones.DomainEvents {
    public class NotificacionCreadaDomainEvent : IDomainEvent
    {
        public Guid NotificacionId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public NotificacionCreadaDomainEvent(Guid notificacionId, Guid usuarioId)
        {
            NotificacionId = notificacionId;
            UsuarioId = usuarioId;
        }
    }
}