using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Core;
using Domain.Hilos.Models.Enums;

namespace Application.Hilos.Queries.GetPortadas
{
    public class GetPortadasQueryHandler : IQueryHandler<GetPortadasQuery, List<GetPortadaResponse>>
    {

        private readonly IDBConnectionFactory _connection;
        private readonly ICurrentUser _user;
        public GetPortadasQueryHandler(IDBConnectionFactory connection, ICurrentUser user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task<Result<List<GetPortadaResponse>>> Handle(GetPortadasQuery request, CancellationToken cancellationToken)
        {

            var sql = @"
                SELECT
                    hilo.id,
                    hilo.titulo,
                    hilo.autor_id,
                    hilo.recibir_notificaciones,
                    hilo.created_at,
                    subcategoria.nombre_corto AS subcategoria,
                    sticky.id IS NOT NULL AS es_sticky,
                    hilo.encuesta_id IS NOT NULL AS tiene_encuesta,
                    hilo.dados_activado,
                    hilo.id_unico_activado,
                    spoileable.spoiler,
                    portada.miniatura as url
                FROM hilos hilo
                    JOIN medias_spoileables spoileable ON spoileable.id = hilo.portada_id
                    JOIN medias portada ON portada.id = spoileable.hashed_media_id
                    LEFT JOIN stickies sticky ON hilo.id = sticky.hilo_id
                    JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                /**where**/
                ORDER BY
                    es_sticky,
                    hilo.ultimo_bump
                LIMIT 20
            ";
            using var connection = _connection.CreateConnection();

            SqlBuilder builder = new SqlBuilder();

            builder.Where("hilo.status = @Status", new {
                Status = HiloStatus.Activo
            });

            if(_user.IsAuthenticated && string.IsNullOrEmpty(request.Titulo) && request.Categoria is null){
                builder.Where("hilo.id NOT IN (SELECT hilo_id FROM hilo_interacciones WHERE usuario_id = @UsuarioId AND oculto = true)", new {_user.UsuarioId });
            } else {
                if(!string.IsNullOrEmpty(request.Titulo)){
                    builder.Where("hilo.titulo ~ @Titulo", new { request.Titulo });
                }

                if(request.UltimaPortada is not null ) {
                    builder.Where("hilo.ultimo_bump < (SELECT ultimo_bump FROM hilos WHERE id = @Id)", new { Id = (Guid) request.UltimaPortada});
                }
            }
            

            if(request.Categoria is not null){

                builder.Where("hilo.subcategoria_id = @Categoria", new { request.Categoria});
            
            } else if( request.CategoriasBloqueadas.Count != 0){
                builder.Where("NOT (hilo.subcategoria_id = ANY (@Subcategorias))", new {Subcategorias = request.CategoriasBloqueadas});
            }

            SqlBuilder.Template template = builder.AddTemplate(sql);

            var portadas = await connection.QueryAsync<GetPortadaResponse,GetBanderas, GetPortadaMiniatura, GetPortadaResponse>(template.RawSql,
                (portada,banderas, imagen ) => {
                    portada.Miniatura = imagen;

                    portada.Banderas = banderas;

                    return portada;
                },
                template.Parameters,
                splitOn: "es_sticky, spoiler"
            );

            return portadas.ToList();
        }
    }
}