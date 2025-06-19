using Application.Notificaciones.Queries.GetNotificacionesQuery;

namespace Application.Notificaciones.Abstractions
{
    public interface INotificacionesHub
    {
        Task EnviarNotificacion(GetNotificacionResponse notificacion, Guid usuario);
    }

}