using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Application.Registros.Queries.GetHilosPosteadosRegistros;
using Dapper;
using Domain.Core;

namespace Application.Registros.Queries.GetComentariosRegistros
{
    public class GetComentariosRegistrosQueryHandler : IQueryHandler<GetComentariosRegistrosQuery, List<RegistroResponse>>
    {

        private readonly IDBConnectionFactory _connection;

        public GetComentariosRegistrosQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<RegistroResponse>>> Handle(GetComentariosRegistrosQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            var registros = await connection.QueryAsync<RegistroResponse>(@"
                SELECT 
                    h.id,
                    h.titulo,
                    c.texto AS contenido,
                    h.created_at,
                    portada.miniatura as miniatura,
                    c.tag as comentario_tag
                FROM comentarios c
                JOIN hilos h ON h.id = c.hilo_id
                JOIN medias_spoileables spoileable ON spoileable.id = h.portada_id
                JOIN medias portada ON portada.id = spoileable.hashed_media_id
                WHERE c.autor_id = @UsuarioId
                ORDER BY c.created_at DESC;
            ", new {
                request.UsuarioId
            });

            return registros.ToList();
        }
    }
}