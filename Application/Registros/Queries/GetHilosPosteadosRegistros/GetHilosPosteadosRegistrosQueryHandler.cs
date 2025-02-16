using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Core;

namespace Application.Registros.Queries.GetHilosPosteadosRegistros
{
    public class GetHilosPosteadosRegistrosQueryHandler : IQueryHandler<GetHilosPosteadosRegistrosQuery, List<RegistroResponse>>
    {

        private readonly IDBConnectionFactory _connection;

        public GetHilosPosteadosRegistrosQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<RegistroResponse>>> Handle(GetHilosPosteadosRegistrosQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            var registros = await connection.QueryAsync<RegistroResponse>(@"
                SELECT 
                    h.id,
                    h.titulo,
                    h.descripcion AS contenido,
                    h.created_at,
                    portada.miniatura
                FROM hilos h
                JOIN medias_spoileables spoileable ON spoileable.id = h.portada_id
                JOIN medias portada ON portada.id = spoileable.hashed_media_id
                WHERE h.autor_id = @UsuarioId
                ORDER BY h.created_at DESC;
            ", new {
                request.UsuarioId
            });

            return registros.ToList();
        }
    }
}