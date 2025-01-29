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
                WITH ultimo_comentario AS (
                    -- Seleccionar los ú ltimos 10 comentarios principales
                    SELECT
                        id
                    FROM
                        comentarios
                    WHERE
                        status = 0 AND hilo_id = @Hilo
                    ORDER BY
                        created_at DESC
                    LIMIT
                        20
                ), comentarios_con_respuestas AS (
                    -- Seleccionar los comentarios principales
                    SELECT
                        comentario.id,
                        comentario.tag,
                        comentario.texto,
                        comentario.created_at,
                        comentario.color,
                       	NULL::uuid AS respondido_por
                    FROM ultimo_comentario uc
                    JOIN comentarios comentario ON comentario.id = uc.id
                    UNION ALL
                    SELECT
                        comentario.id,
                        comentario.tag,
                        comentario.texto,
                        comentario.created_at,
                        comentario.color,
                        respondido.respuesta_id AS respondido_por
                    FROM
                        comentarios comentario
                        JOIN respuesta_comentario respondido ON respondido.respondido_id = comentario.id
                        JOIN comentarios_con_respuestas cr ON cr.id = respondido.respuesta_id
                    WHERE
                        comentario.status = 0
                )
                SELECT
                    *
                FROM
                    comentarios_con_respuestas
                ORDER BY
                    created_at DESC;
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDic = new Dictionary<Guid, GetComentarioResponse>();

            List<GetComentarioResponse> _comentarios = [];

            var comentarios = await connection.QueryAsync<GetComentarioResponse>(sql, new {
                request.Hilo
            });

            foreach (var c in comentarios)
            {
                if(c.Respondido_Por is null){
                    _comentariosDic.Add(c.Id, c);
                    _comentarios.Add(c);
                } else {
                    _comentariosDic[(Guid)c.Respondido_Por].Responde.Add(c);
                }
            }

            return _comentarios.ToList();
        }
    }
}