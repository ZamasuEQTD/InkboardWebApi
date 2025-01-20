using Domain.Core;

namespace Domain.Notificaciones.ValueObjects
{
     public class NotificacionId : EntityId
    {
        public NotificacionId(Guid id) : base(id) { }
    }
}