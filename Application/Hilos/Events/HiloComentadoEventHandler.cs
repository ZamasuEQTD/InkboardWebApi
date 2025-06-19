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
                    c.*
                FROM vw_comentarios_con_relaciones c
                WHERE
                    c.hilo_id = @HiloId AND c.id = @ComentarioId
               
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDic = new Dictionary<Guid, GetComentarioResponse>();

            var comentarios = await connection.QueryAsync<GetComentarioResponse, GetMediaResponse?, string, string, GetComentarioResponse>(sql,
                        (comentario, media, responde, respondido) =>
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
            splitOn: "url,responde_a_tag,respondido_por_tag");

            await _hiloHub.NotificarHiloComentado(notification.HiloId, comentarios.First());
        }
    }
}