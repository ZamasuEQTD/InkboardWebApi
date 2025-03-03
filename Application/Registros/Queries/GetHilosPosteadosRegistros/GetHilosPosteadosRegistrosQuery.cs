using Application.Core.Abstractions.Messaging;

namespace Application.Registros.Queries.GetHilosPosteadosRegistros
{
    public class GetHilosPosteadosRegistrosQuery : IQuery<List<RegistroResponse>>
    {
        public Guid UsuarioId {get;set;}
        public Guid? Hilo {get;set;}
    }


    public class RegistroResponse 
    {
        public Guid? Comentario_Id {get;set;}
        public Guid Id {get;set;}
        public DateTime Created_at {get;set;}
        public string Miniatura {get;set;}
        public string Titulo {get;set;}
        public string Contenido {get;set;}
        public string? Comentario_Tag {get;set;}
    }
}