using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Responses;
using Dapper;
using Domain.Core;

namespace Application.Comentarios.Queries.GetComentarios
{

    public class GetComentariosQueryHandler : IQueryHandler<GetComentariosQuery, List<GetComentarioResponse>>
    {
        private readonly IDBConnectionFactory _connection;

        private readonly ICurrentUser _user;

        public GetComentariosQueryHandler(IDBConnectionFactory connection, ICurrentUser user)
        {
            _connection = connection;
            _user = user;
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
                    c.autor_role,
                    c.autor_username AS autor,
                    c.autor_id,
                    h.autor_id as autor_hilo_id,
                    c.dados,
                    c.created_at,
                    respuesta.tag as respondido,
                    responde.tag as responde,
                    media.url,
                    media.previsualizacion,
                    media.provider,
                    spoiler.spoiler
                FROM
                    (
                        SELECT
                            c.*
                        FROM
                            comentarios c
                        WHERE status = 0 AND hilo_id = @Hilo
                        ORDER BY created_at DESC
                    ) AS c
                    JOIN hilos h ON h.id = c.hilo_id
                    LEFT JOIN medias_spoileables spoiler ON c.media_id = spoiler.id
                    LEFT JOIN medias media ON spoiler.hashed_media_id = media.id
                    LEFT JOIN respuesta_comentario rc ON rc.respondido_id = c.id
                    LEFT JOIN comentarios respuesta ON rc.respuesta_id = respuesta.id  
                    LEFT JOIN respuesta_comentario cr ON cr.respuesta_id = c.id
                    LEFT JOIN comentarios responde ON cr.respondido_id = responde.id  

                ORDER BY
                    created_at DESC
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDic = new Dictionary<Guid, GetComentarioResponse>();

            var comentarios = await connection.QueryAsync<GetComentarioResponse, string, string,GetMediaResponse?, GetComentarioResponse>(sql, 
                        (comentario, respondido, responde,media) => 
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

                comentarioEntry.Media = media;

                return comentarioEntry;
            },new {
                request.Hilo
            },
            splitOn: "respondido,responde,url");

            var comentariosList = _comentariosDic.Values.Select(c=> {

                c.Es_Op = c.Autor_Id == c.Autor_Hilo_Id;

                c.Es_Autor = this._user.IsAuthenticated && this._user.UsuarioId == c.Autor_Id;
               
                return c;
            }
            ).ToList();

            return Result.Success(comentariosList);
        }
    }
}