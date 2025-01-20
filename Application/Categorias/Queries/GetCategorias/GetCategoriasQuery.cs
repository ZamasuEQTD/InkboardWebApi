using System.Text.Json.Serialization;
using Application.Core.Abstractions.Messaging;

namespace Application.Categorias.Queries.GetCategorias
{
    public class GetCategoriasQuery : IQuery<List<GetCategoriaReponse>>
    {

    }

    public class GetCategoriaReponse
    {
        public  Guid Id { get; set; }
        public string Nombre { get; set; }
        public List<GetSubcategoriaResponse> Subcategorias { get; set; } = [];
    }

    public class GetSubcategoriaResponse
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Imagen => "https://i.pinimg.com/564x/66/39/3c/66393c7bf72ac71bd1fc070be8b9cd52.jpg";
    }
}