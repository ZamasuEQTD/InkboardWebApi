using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Dapper;
using Domain.Core;

namespace Application.Categorias.Queries.GetCategorias
{
    public class GetCategoriasQueryHandler : IQueryHandler<GetCategoriasQuery, List<GetCategoriaReponse>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetCategoriasQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetCategoriaReponse>>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
        {
            Dictionary<Guid, GetCategoriaReponse> _categorias = [];

            string sql = @"
            SELECT
                id,
                nombre
            FROM categorias;
            SELECT
                id,
                categoria_id AS CategoriaId,
                nombre
            FROM subcategorias 
            ";

            using var connection = _connection.CreateConnection();

            using var query = await connection.QueryMultipleAsync(sql);

            var c = query.Read<GetCategoriaReponse>().ToList();

            var s = query.Read<GetSubcategoriaResponse>();
            
            foreach (var sub in s)
            {
                GetCategoriaReponse? categoria = c.FirstOrDefault(c => c.Id == sub.CategoriaId);

                if (categoria != null)
                {
                    categoria.Subcategorias.Add(sub);
                }
            }

            return c.ToList();
        }
    }
}