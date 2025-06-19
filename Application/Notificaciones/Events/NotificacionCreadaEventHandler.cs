using Application.Core.Abstractions;
using Application.Notificaciones.Abstractions;
using Application.Notificaciones.Queries.GetNotificacionesQuery;
using Dapper;
using Domain.Notificaciones.DomainEvents;
using MediatR;

namespace Application.Notificaciones.Events
{
    public class NotificacionCreadaEventHandler : INotificationHandler<NotificacionCreadaDomainEvent>
    {

        private readonly IDBConnectionFactory _connection;

        private readonly INotificacionesHub _hub;
        public NotificacionCreadaEventHandler(IDBConnectionFactory connection, INotificacionesHub hub)
        {
            _connection = connection;
            _hub = hub;
        }


        public async Task Handle(NotificacionCreadaDomainEvent notification, CancellationToken cancellationToken)
        {

            using var connection = _connection.CreateConnection();

            IEnumerable<GetNotificacionResponse> notificaciones = await connection.QueryAsync<GetNotificacionResponse, GetHiloNotificacionResponse, GetNotificacionResponse>(
                @"
            SELECT 
                notificacion.tipo_de_interaccion as tipo,
                notificacion.id,
                notificacion.created_at as fecha,
                respuesta.tag AS Comentario_Respuesta_Tag,
                respondido.tag AS Comentario_Respondido_Tag,
                respuesta.texto AS contenido,
                h.id as id,
                h.titulo as titulo,
                p.miniatura as portada
            FROM notificaciones notificacion
            JOIN hilos h ON notificacion.hilo_id = h.id
            JOIN comentarios respuesta ON notificacion.comentario_id = respuesta.id
            JOIN medias_spoileables spoiler ON h.portada_id = spoiler.id
            JOIN medias p ON spoiler.hashed_media_id = p.id
            LEFT JOIN comentarios respondido ON respondido.id = notificacion.respondido_id
            WHERE 
                notificacion.id = @NotificacionId

            ORDER BY fecha DESC
            LIMIT 20
            ",
                (notificacion, hilo) =>
                {
                    notificacion.Hilo = hilo;

                    return notificacion;
                },
                new { notification.NotificacionId, }
            );

            var notificacion = notificaciones.ToList().FirstOrDefault();


            if (notificacion is not null)
            {
                await this._hub.EnviarNotificacion(notificacion, notification.UsuarioId);
            }
        }
    }
}