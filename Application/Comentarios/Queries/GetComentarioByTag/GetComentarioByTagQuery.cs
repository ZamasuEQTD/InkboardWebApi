using Application.Comentarios.Queries.GetComentarios;
using Application.Core.Abstractions.Messaging;

namespace Application.Comentarios.Queries.GetComentarioByTag
{
    public class GetComentarioByTagQuery : IQuery<GetComentarioResponse>
    {
        public Guid Hilo{get;set;}
        public string Tag {get;set;}
    }
}