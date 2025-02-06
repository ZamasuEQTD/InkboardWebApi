using Domain.Notificaciones.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Notificaciones
{
    public interface INotificacionesRepository
    {
        Task<Notificacion?> GetNotificacionById(NotificacionId id);
        Task<List<Notificacion>> GetNotificacionesDeUsuarioById(IdentityId id);

    }
}