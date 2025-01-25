using Application.Categorias.Queries.GetCategorias;
using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
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

            var responses = await  connection.QueryAsync<GetHiloResponse,GetHiloMediaResponse,GetSubcategoriaResponse,GetEncuestaResponse,GetEncuestaRespuestaResponse, GetHiloResponse>(@"
                WITH HilosFiltrados AS (
                    SELECT *
                    FROM hilos
                    WHERE id = @HiloId AND status = 0
                ),
                ComentariosAgregados AS (
                    SELECT
                        hilo_id,
                        SUM(CASE status WHEN 0 THEN 1 ELSE 0 END) AS cantidad_comentarios
                    FROM comentarios
                    GROUP BY hilo_id
                ),
                VotosAgregados AS (
                    SELECT
                        respuesta_id,
                        COUNT(id) AS votos
                    FROM votos
                    GROUP BY respuesta_id
                )
                SELECT
                    hilo.id,
                    hilo.titulo,
                    hilo.descripcion,
                    COALESCE(comentarios.cantidad_comentarios, 0) AS cantidad_comentarios,
                    hilo.autor_id,
                    hilo.recibir_notificaciones,
                    portada.url,
                    portada.previsualizacion,
                    spoiler.spoiler,
                    portada.provider,
                    subcategoria.id,
                    subcategoria.nombre,
                    encuesta.id,
                    voto_usuario.respuesta_id AS respuesta_votada,
                    respuesta.id,
                    respuesta.contenido AS respuesta,
                    COALESCE(votos.votos, 0) AS votos
                FROM HilosFiltrados hilo
                JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                JOIN medias_spoileables spoiler ON hilo.portada_id = spoiler.id
                JOIN medias portada ON spoiler.hashed_media_id = portada.id
                LEFT JOIN ComentariosAgregados comentarios ON hilo.id = comentarios.hilo_id
                LEFT JOIN encuestas encuesta ON hilo.encuesta_id = encuesta.id
                LEFT JOIN respuestas respuesta ON encuesta.id = respuesta.encuesta_id
                LEFT JOIN VotosAgregados votos ON respuesta.id = votos.respuesta_id
                LEFT JOIN votos voto_usuario ON respuesta.id = voto_usuario.respuesta_id AND voto_usuario.votante_id = @UsuarioId;
            ", (hilo, media, subcategoria, encuesta, respuesta) => {

                Console.WriteLine(encuesta);
                hilo.Media = media;

                hilo.Subcategoria = subcategoria;

                if(_encuesta is not null){
                    encuesta = _encuesta;
                } else {
                    _encuesta = encuesta;
                }

                if(respuesta is not null){
                    _encuesta!.Respuestas.Add(respuesta);
                }
            
                hilo.Encuesta = _encuesta;

                return hilo;
            }, new {
                request.HiloId,
                UsuarioId = _user.IsAuthenticated? (Guid?) _user.UsuarioId : null
            },splitOn : "id, url, id, id, id");


            GetHiloResponse? hilo = responses.FirstOrDefault();

            if(hilo is null) return HiloErrors.NoEncontrado;


            hilo.Recibir_Notificaciones = _user.IsAuthenticated && _user.UsuarioId == hilo.Autor_Id? hilo.Recibir_Notificaciones : null; 

            hilo.Es_Op = _user.IsAuthenticated && _user.UsuarioId == hilo.Autor_Id; 

            hilo.Autor_Id = _user.IsAuthenticated && _user.EsModerador? hilo.Autor_Id : null;


            return hilo;
        }
    }
}