using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Responses;
using Dapper;
using Domain.Core;

namespace Application.Comentarios.Queries.GetComentarios
{

    public class GetComentariosQueryHandler : IQueryHandler<GetComentariosQuery, GetComentariosDeHiloResponse>
    {
        private readonly IDBConnectionFactory _connection;

        private readonly ICurrentUser _user;

        public GetComentariosQueryHandler(IDBConnectionFactory connection, ICurrentUser user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task<Result<GetComentariosDeHiloResponse>> Handle(GetComentariosQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            var destacadossql = @"
                SELECT
                    c.*,
                    respuesta.tag as respondido,
                    responde.tag as responde
                FROM vw_comentarios_destacados_en_hilo c
                LEFT JOIN respuesta_comentario rc ON rc.respondido_id = c.id
                LEFT JOIN comentarios respuesta ON rc.respuesta_id = respuesta.id  
                LEFT JOIN respuesta_comentario cr ON cr.respuesta_id = c.id
                LEFT JOIN comentarios responde ON cr.respondido_id = responde.id
                WHERE c.hilo_id = @Hilo
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDestacadosDic = new Dictionary<Guid, GetComentarioResponse>();

            var destacados = await connection.QueryAsync<GetComentarioResponse,  GetMediaResponse?, string, string, GetComentarioResponse>(destacadossql,
                        (comentario, media, respondido, responde) =>
            {
                if (!_comentariosDestacadosDic.TryGetValue(comentario.Id, out var comentarioEntry))
                {
                    comentarioEntry = comentario;
                    _comentariosDestacadosDic.Add(comentarioEntry.Id, comentarioEntry);
                }

                if (responde is not null && !comentarioEntry.Responde_A.Contains(responde))
                {
                    comentarioEntry.Responde_A.Add(responde);
                }

                if (respondido is not null && !comentarioEntry.Respondido_Por.Contains(respondido))
                {
                    comentarioEntry.Respondido_Por.Add(respondido);
                }

                comentarioEntry.Media = media;

                return comentarioEntry;
            }, new
            {
                request.Hilo,
            }, splitOn: "url,respondido,responde");

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
                    c.status = 0  AND c.hilo_id = @Hilo AND
                    (NOT @IsAuthenticated OR c.id NOT IN (
                        SELECT 
                            comentario_id 
                        FROM comentario_interracion i
                        WHERE i.usuario_id = @UsuarioId AND i.oculto AND i.comentario_id = c.id
                    ))
                ORDER BY
                    created_at DESC
            ";

            Dictionary<Guid, GetComentarioResponse> _comentariosDic = new Dictionary<Guid, GetComentarioResponse>();

            var comentarios = await connection.QueryAsync<GetComentarioResponse,GetMediaResponse?,string, string, GetComentarioResponse>(sql,
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
                request.Hilo,
                this._user.IsAuthenticated,
                usuarioId = this._user.IsAuthenticated ? (Guid?)_user.UsuarioId : null
            },
            splitOn: "url, respondido, responde");

            var destacadosList = _comentariosDestacadosDic.Values.Select(c =>
            {

                c.Destacado = true;

                c.Es_Op = c.Autor_Id == c.Autor_Hilo_Id;

                c.Es_Autor = this._user.IsAuthenticated && this._user.UsuarioId == c.Autor_Id;

                return c;
            }).ToList();

            var comentariosList = _comentariosDic.Values.Select(c =>
            {

                c.Es_Op = c.Autor_Id == c.Autor_Hilo_Id;

                c.Es_Autor = this._user.IsAuthenticated && this._user.UsuarioId == c.Autor_Id;

                return c;
            }
            ).ToList();

            return new GetComentariosDeHiloResponse()
            {
                Comentarios = comentariosList,
                Destacados = destacadosList
            };
        }
    }
}