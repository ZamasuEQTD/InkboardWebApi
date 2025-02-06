using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Core;

namespace Application.Notificaciones.Queries.GetNotificacionesQuery
{
    public class GetNotificacionesQueryHandler : IQueryHandler<GetNotificacionesQuery, List<GetNotificacionResponse>>
    {
        private readonly IDBConnectionFactory _connection;
        private readonly ICurrentUser _user;

        public GetNotificacionesQueryHandler(ICurrentUser user, IDBConnectionFactory connection)
        {
            _user = user;
            _connection = connection;
        }

        public async Task<Result<List<GetNotificacionResponse>>> Handle(GetNotificacionesQuery request, CancellationToken cancellationToken)
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
                CASE 
                    WHEN notificacion.tipo_de_interaccion = 'ComentarioRespondido' THEN respuesta.texto
                    ELSE h.titulo
                END AS contenido,
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
                notificacion.notificado_id = @UsuarioId
            AND 
                notificacion.leida = false
            AND 
                (@UltimaNotificacion IS NULL OR
                notificacion.created_at < (
                    SELECT 
                        created_at
                    FROM notificaciones
                    WHERE id = @UltimaNotificacion
                )
                )
            ORDER BY fecha DESC
            LIMIT 20
            ",
                (notificacion, hilo) =>
                {
                    notificacion.Hilo = hilo;

                    return notificacion;
                },
                new { _user.UsuarioId, request.UltimaNotificacion }
            );

            return notificaciones.ToList();
        }
    }
}