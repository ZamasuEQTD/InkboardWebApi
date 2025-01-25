using Application.Categorias.Queries.GetCategorias;
using Application.Core.Abstractions.Messaging;
using Application.Encuestas.Queries.Responses;

namespace Application.Hilos.Queries.GetHilo
{

    public class GetHiloQuery : IQuery<GetHiloResponse>
    {
        public Guid HiloId {get;set;}
    }
    public class GetHiloResponse {
        public Guid Id {get;set;}
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Guid? Autor_Id { get; set; }
        public bool Es_Op { get; set; }
        public int Cantidad_Comentarios { get; set; }
        public bool? Recibir_Notificaciones {get;set;}
        public GetSubcategoriaResponse Subcategoria {get;set;}
        public GetHiloMediaResponse Media {get;set;}
        public GetEncuestaResponse? Encuesta {get;set;}
    }   


    public class GetHiloMediaResponse
    {
        public string Provider { get; set; }
        public bool Spoiler { get; set; }
        public string Url { get; set; }
        public string? Previsualizacion { get; set; }
    }
}