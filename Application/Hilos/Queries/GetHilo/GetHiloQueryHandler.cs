using Application.Categorias.Queries.GetCategorias;
using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Responses;
using Application.Encuestas.Queries.Responses;
using Dapper;
using Domain.Core;
using Domain.Hilos.Models;

namespace Application.Hilos.Queries.GetHilo {
    public class GetHiloQueryHandler : IQueryHandler<GetHiloQuery, GetHiloResponse>
    {
        private readonly IDBConnectionFactory _connection;
        private readonly ICurrentUser _user;
        public GetHiloQueryHandler(IDBConnectionFactory connection, ICurrentUser user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task<Result<GetHiloResponse>> Handle(GetHiloQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            GetEncuestaResponse? _encuesta = null;

            var responses = await connection.QueryAsync<GetHiloResponse, GetMediaResponse, GetSubcategoriaResponse, GetEncuestaResponse, GetEncuestaRespuestaResponse, GetHiloResponse>(
                @"
SELECT
    hilo.id,
    hilo.titulo,
    hilo.descripcion,
    hilo.created_at,
    hilo.autor_role,
    hilo.autor_username AS autor,
    hilo.autor_id,
    hilo.recibir_notificaciones,
    COALESCE(comentarios_agrupados.cantidad_comentarios, 0) AS cantidad_comentarios,
    portada.url,
    portada.previsualizacion,
    portada.spoiler,
    portada.provider,
    subcategoria.id,
    subcategoria.nombre,
    encuesta.id,
    votos_agrupados.respuesta_votada,
    encuesta.respuesta_id as id,
    encuesta.respuesta,
    encuesta.votos
FROM
    hilos hilo
JOIN subcategorias subcategoria ON hilo.subcategoria_id = subcategoria.id
JOIN vw_spoileable_hashed_medias portada ON hilo.portada_id = portada.id
LEFT JOIN vw_encuestas encuesta ON hilo.encuesta_id = encuesta.id
LEFT JOIN (
    SELECT
        hilo_id,
        COUNT(*) AS cantidad_comentarios
    FROM
        comentarios
    WHERE
        status = 0
    GROUP BY
        hilo_id
) AS comentarios_agrupados ON hilo.id = comentarios_agrupados.hilo_id
LEFT JOIN (
    SELECT
        encuesta_id,
        respuesta_id AS respuesta_votada
    FROM
        votos
    WHERE
        votante_id = @UsuarioId
) AS votos_agrupados ON hilo.encuesta_id = votos_agrupados.encuesta_id
WHERE
    hilo.id = @HiloId AND hilo.status = 0;
                ",
                (hilo, media, subcategoria, encuesta, respuesta) =>
                {
                    hilo.Media = media;
                    hilo.Subcategoria = subcategoria;

                    if (_encuesta is not null)
                    {
                        encuesta = _encuesta;
                    }
                    else
                    {
                        _encuesta = encuesta;
                    }

                    if (respuesta is not null)
                    {
                        _encuesta!.Respuestas.Add(respuesta);
                    }

                    hilo.Encuesta = _encuesta;

                    return hilo;
                },
                new
                {
                    request.HiloId,
                    UsuarioId = _user.IsAuthenticated ? (Guid?)_user.UsuarioId : null
                },
                splitOn: "id, url, id, id, id"
            );


            GetHiloResponse? hilo = responses.FirstOrDefault();

            if(hilo is null) return HiloErrors.NoEncontrado;


            hilo.Recibir_Notificaciones = _user.IsAuthenticated && _user.UsuarioId == hilo.Autor_Id? hilo.Recibir_Notificaciones : null; 

            hilo.Es_Op = _user.IsAuthenticated && _user.UsuarioId == hilo.Autor_Id; 

            hilo.Autor_Id = _user.IsAuthenticated && _user.EsModerador? hilo.Autor_Id : null;


            return hilo;
        }
    }
}