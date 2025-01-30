using System.Formats.Asn1;
using Application.Comentarios.Queries.GetComentarios;
using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Comentarios.Models;
using Domain.Core;

namespace Application.Comentarios.Queries.GetComentarioByTag
{
    public class GetComentarioByTagQueryHandler : IQueryHandler<GetComentarioByTagQuery, GetComentarioResponse>
    {
        private readonly IDBConnectionFactory _connection;

        public GetComentarioByTagQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<GetComentarioResponse>> Handle(GetComentarioByTagQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();
        

            GetComentarioResponse? comentarioEncontrado = null;
        
            var comentarios = await connection.QueryAsync<GetComentarioResponse, string, string, GetComentarioResponse>(@"SELECT
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
                            status = 0 AND hilo_id = @hilo AND tag = @tag
                        ORDER BY
                            created_at DESC
                        LIMIT 20
                    ) AS c
                    LEFT JOIN medias_spoileables ms ON c.media_id = ms.id
                    LEFT JOIN respuesta_comentario rc ON rc.respondido_id = c.id
                    LEFT JOIN comentarios respuesta ON rc.respuesta_id = respuesta.id  
                    LEFT JOIN respuesta_comentario cr ON cr.respuesta_id = c.id
                    LEFT JOIN comentarios responde ON cr.respondido_id = responde.id  
                ORDER BY created_at DESC
                ",
                (comentario, respondido, responde) => {
                    if(comentarioEncontrado is null){
                        comentarioEncontrado = comentario;
                    }
                    
                    if(respondido is not null) {
                        comentarioEncontrado.Respondido_Por.Add(respondido);
                    }

                    if(responde is not null){
                        comentarioEncontrado.Responde_A.Add(responde);
                    }

                    return comentario;
                },
                new {
                    request.Hilo,
                    request.Tag
                },
                splitOn: "respondido,responde"
            );

            if(comentarioEncontrado is null) return ComentarioErrors.NoEncontrado;
            
            return comentarioEncontrado;
        }
    }
}