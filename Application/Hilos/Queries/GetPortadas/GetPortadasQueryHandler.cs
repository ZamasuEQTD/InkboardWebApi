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
                SELECT * FROM HiloPortadaView                     
                /**where**/
                ORDER BY
                    es_sticky,
                    ultimo_bump
                LIMIT 20
            ";
            using var connection = _connection.CreateConnection();

            SqlBuilder builder = new SqlBuilder();

            builder.Where("status = @Status", new {
                Status = HiloStatus.Activo
            });

            if(_user.IsAuthenticated && string.IsNullOrEmpty(request.Titulo) && request.Categoria is null){
                builder.Where("id NOT IN (SELECT hilo_id FROM hilo_interacciones WHERE usuario_id = @UsuarioId AND oculto = true)", new {_user.UsuarioId });
            } else {
                if(!string.IsNullOrEmpty(request.Titulo)){
                    builder.Where("titulo ~ @Titulo", new { request.Titulo });
                }

                if(request.UltimaPortada is not null ) {
                    builder.Where("ultimo_bump < (SELECT ultimo_bump FROM hilos WHERE id = @Id)", new { Id = (Guid) request.UltimaPortada});
                }
            }
            

            if(request.Categoria is not null){

                builder.Where("subcategoria_id = @Categoria", new { request.Categoria});
            
            } else if( request.CategoriasBloqueadas.Count != 0){
                builder.Where("NOT (subcategoria_id = ANY (@Subcategorias))", new {Subcategorias = request.CategoriasBloqueadas});
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