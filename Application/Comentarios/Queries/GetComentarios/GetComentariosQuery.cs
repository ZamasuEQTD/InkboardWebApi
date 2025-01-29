using System.Text.Json.Serialization;
using Application.Core.Abstractions.Messaging;

namespace Application.Comentarios.Queries.GetComentarios
{
    public class GetComentariosQuery : IQuery<List<GetComentarioResponse>>
    {
        public Guid UltimoComentario { get; set; }   
        public Guid Hilo { get; set; }
    }


    public class GetComentarioResponse
    {
        public Guid Id {get;set;}
        public string Texto {get;set;}
        public string Tag {get;set;}
        public string? Dados {get;set;}
        public string? Tag_Unico {get;set;}
        public string Color {get;set;}
        [JsonIgnore]
        public Guid? Respondido_Por {get;set;}
        public DateTime Created_At {get;set;}
        public List<GetComentarioResponse> Responde {get;set;} = [];
    }
}