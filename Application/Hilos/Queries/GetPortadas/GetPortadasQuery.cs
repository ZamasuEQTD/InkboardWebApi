using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Queries.GetPortadas
{
    public class GetPortadasQuery : IQuery<List<GetPortadaResponse>>
    {
        public Guid? UltimaPortada {get; set;}
        public string? Titulo {get;set;}
        public Guid? Categoria {get;set;}
        public List<Guid> CategoriasBloqueadas = [];
    }


    public class GetPortadaResponse 
    {
        public Guid Id {get; set;}
        public Guid? Autor_Id {get; set;}
        public bool? Recibir_Notificaciones {get;set;}
        public bool Es_Op {get;set;}
        public string Titulo {get;set;}
        public string Subcategoria {get;set;}
        public GetPortadaMiniatura Miniatura {get;set;}
        public GetBanderas Banderas {get;set;}
    }

    public class GetPortadaMiniatura
    {
        public bool Spoiler {get; set;}
        public string Url {get;set;}
    }

    public class GetBanderas
    {
        public bool Es_Sticky {get;set;}
        public bool Tiene_Encuesta {get;set;}
        public bool Dados_Activado {get;set;}
        public bool Id_Unico_Activado {get;set;}
    }
}