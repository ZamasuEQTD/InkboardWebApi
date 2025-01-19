using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Core.Exceptions;
using Application.Encuestas.Queries.Responses;
using Dapper;
using Domain.Comentarios;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Hilos.Queries.GetHilo;

public class GetHiloQueryHandler : IQueryHandler<GetHiloQuery, GetHiloResponse>
{

    private readonly IDBConnectionFactory _connection;
    private readonly ICurrentUser _user;
    public GetHiloQueryHandler(ICurrentUser user, IDBConnectionFactory connection)
    {
        _user = user;
        _connection = connection;
    }

    public async Task<Result<GetHiloResponse>> Handle(GetHiloQuery request, CancellationToken cancellationToken)
    {
        var sql =  @"
            SELECT
                hilo.id,
                hilo.titulo,
                hilo.descripcion,
                hilo.autor_id,
                hilo.encuesta_id,
                hilo.created_at,
                hilo.recibir_notificaciones,
                portada.url,
                portada.previsualizacion,
                spoiler.spoiler,
                portada.provider,
                subcategoria.id,
                subcategoria.nombre
            FROM hilos hilo
            JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
            JOIN medias_spoileables spoiler ON hilo.portada_id = spoiler.id
            JOIN medias portada ON spoiler.hashed_media_id = portada.id
        ";

        using var connection = _connection.CreateConnection();

        var hilo = await connection.QueryFirstAsync<GetHilo?>(sql);      

        if(hilo is null) throw new InvalidCommandException("Hilo no encontrado");

        if(hilo.Encuesta_Id is not null) {
            GetEncuestaResponse? encuesta = null;
            
            var rows = await connection.QueryAsync<GetEncuestaResponse, GetEncuestaRespuestaResponse, GetEncuestaResponse>(
                @"
                SELECT 
                    encuesta.id,
                    respuesta.id,
                    respuesta.contenido,
                    count(voto.id) AS votos
                FROM encuestas encuesta
                JOIN respuestas respuesta ON respuesta.encuesta_id = encuesta.id
                JOIN votos voto ON voto.respuesta_id = respuesta.id
                GROUP BY encuesta.id, respuesta.id
                ",
                (e, respuesta) =>
                {
                    if(encuesta is null) {
                        encuesta = e;
                    } else {
                        e = encuesta;
                    }

                    e.Respuestas.Add(respuesta);

                    return e;
                },
                splitOn: "id"
            );

            hilo.Encuesta = encuesta;
        }

        throw new Exception("");   
    }


    public class GetHilo
    {
        public Guid Id {get;set;}
        public string Titulo {get;set;}
        public string Descripcion {get;set;}
        public Guid? Autor_Id {get;set;}
        public Guid? Encuesta_Id {get;set;}
        public bool? Recibir_Notificaciones {get;set;}
        public  GetEncuestaResponse? Encuesta {get;set;}
    }
}
