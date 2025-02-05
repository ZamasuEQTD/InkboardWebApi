using System.Text.Json.Serialization;
using Application.Core.Abstractions.Messaging;
using Application.Core.Responses;

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
        public bool Es_Op {get;set;}
        public bool Es_Autor {get;set;}
        public Guid Autor_Id {get;set;}
        [JsonIgnore]
        public Guid Autor_Hilo_Id {get;set;}
        public DateTime Created_At {get;set;}
        public HashSet<string> Responde_A {get;set;} = [];
        public HashSet<string> Respondido_Por {get;set;} = [];
        [JsonIgnore]
        public string Responde {get;set;}
        [JsonIgnore]
        public string Respondido {get;set;}
        public GetMediaResponse? Media {get;set;}
    }

   
}