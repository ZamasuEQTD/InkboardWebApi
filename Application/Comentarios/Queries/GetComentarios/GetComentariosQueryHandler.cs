using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Core;

namespace Application.Comentarios.Queries.GetComentarios
{

    public class GetComentariosQueryHandler : IQueryHandler<GetComentariosQuery, List<GetComentarioResponse>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetComentariosQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetComentarioResponse>>> Handle(GetComentariosQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            var sql = @"
                SELECT
                    c.id,
                    c.texto,
                    c.tag,
                    c.color,
                    c.tag_unico,
                    c.dados,
                    c.created_at,
                    respuesta.tag as respondido,
                    responde.tag as responde
                FROM
                    (
                        SELECT
                            *
                        FROM
                            comentarios
                        WHERE
                            status = 0
                        ORDER BY
                            created_at DESC
                        LIMIT 20
                    ) AS c
                    LEFT JOIN medias_spoileables ms ON c.media_id = ms.id
                    LEFT JOIN respuesta_comentario rc ON rc.respondido_id = c.id
                    LEFT JOIN comentarios respuesta ON rc.respuesta_id = respuesta.id  
                    LEFT JOIN respuesta_comentario cr ON cr.respuesta_id = c.id
                    LEFT JOIN comentarios responde ON cr.respondido_id = responde.id  
                
                ORDER BY
                    created_at DESC
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDic = new Dictionary<Guid, GetComentarioResponse>();

            var comentarios = await connection.QueryAsync<GetComentarioResponse, string, string, GetComentarioResponse>(sql, 
                        (comentario, respondido, responde) => 
            {
                if (!_comentariosDic.TryGetValue(comentario.Id, out var comentarioEntry))
                {
                    comentarioEntry = comentario;
                    _comentariosDic.Add(comentarioEntry.Id, comentarioEntry);
                }

                if(responde is not null && !comentarioEntry.Responde_A.Contains(responde)) {
                    comentarioEntry.Responde_A.Add(responde);
                }   
        
                if(respondido is not null && !comentarioEntry.Respondido_Por.Contains(respondido)) {
                    comentarioEntry.Respondido_Por.Add(respondido);
                }

                return comentarioEntry;
            },
            splitOn: "respondido,responde");

            return Result<List<GetComentarioResponse>>.Success(_comentariosDic.Values.ToList());
        }
    }
}