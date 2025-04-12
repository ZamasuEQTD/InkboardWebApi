using Application.Comentarios.Queries.GetComentarios;
using Application.Core.Abstractions;
using Application.Core.Responses;
using Application.Hilos.Abstractions;
using Dapper;
using Domain.Hilos.DomainEvents;
using MediatR;

namespace Application.Hilos.Events
{
    public class HiloComentadoDomainEventHandler : INotificationHandler<HiloComentadoDomainEvent>
    {
        private readonly IHiloHub _hiloHub;
        private readonly IDBConnectionFactory _connection;

        public HiloComentadoDomainEventHandler(
            IHiloHub hiloHub,
            IDBConnectionFactory connection
        )
        {
            _hiloHub = hiloHub;
            _connection = connection;
        }
        public async Task Handle(HiloComentadoDomainEvent notification, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            var sql = @"
                SELECT 
                    c.*,
                    respuesta.tag as respondido,
                    responde.tag as responde
                FROM vw_comentarios_en_hilo c
                LEFT JOIN respuesta_comentario rc ON rc.respondido_id = c.id
                LEFT JOIN comentarios respuesta ON rc.respuesta_id = respuesta.id  
                LEFT JOIN respuesta_comentario cr ON cr.respuesta_id = c.id
                LEFT JOIN comentarios responde ON cr.respondido_id = responde.id
                WHERE
                    c.hilo_id = @HiloId
                    AND c.id = @ComentarioId
               
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDic = new Dictionary<Guid, GetComentarioResponse>();

            var comentarios = await connection.QueryAsync<GetComentarioResponse, GetMediaResponse?, string, string, GetComentarioResponse>(sql,
                        (comentario, media, respondido, responde) =>
            {
                if (!_comentariosDic.TryGetValue(comentario.Id, out var comentarioEntry))
                {
                    comentarioEntry = comentario;
                    _comentariosDic.Add(comentarioEntry.Id, comentarioEntry);
                }

                if (responde is not null && !comentarioEntry.Responde_A.Contains(responde))
                {
                    comentarioEntry.Responde_A.Add(responde);
                }

                Console.WriteLine(respondido);

                if (respondido is not null && !comentarioEntry.Respondido_Por.Contains(respondido))
                {

                    comentarioEntry.Respondido_Por.Add(respondido);
                }

                comentarioEntry.Media = media;

                return comentarioEntry;
            }, new
            {
                notification.HiloId,
                notification.ComentarioId
            },
            splitOn: "url, respondido, responde");

            await _hiloHub.NotificarHiloComentado(notification.HiloId, comentarios.First());
        }
    }
}