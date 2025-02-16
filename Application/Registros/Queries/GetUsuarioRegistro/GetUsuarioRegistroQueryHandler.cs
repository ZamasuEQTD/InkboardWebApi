using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Core;

namespace Application.Registros.Queries.GetUsuarioRegistro
{
    public class GetUsuarioRegistroQueryHandler : IQueryHandler<GetUsuarioRegistroQuery, UsuarioRegistroResponse>
    {
        private readonly IDBConnectionFactory _connection;

        public GetUsuarioRegistroQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<UsuarioRegistroResponse>> Handle(GetUsuarioRegistroQuery request, CancellationToken cancellationToken)
        {

            using var connection = _connection.CreateConnection();

            var registro = await connection.QueryAsync<UsuarioRegistroResponse>(@"
                SELECT 
                    u.registrado_en,
                    u.staff_name
                FROM ""AspNetUsers"" u
                WHERE u.id = @UsuarioId
            ",
            new {
                request.UsuarioId
            }
            );

            return registro.First();
        }
    }
}