using Domain.Comentarios.Models.ValueObjects;
using Domain.Core.Abstractions;

namespace Domain.Comentarios.Models
{
    public class RespuestaComentario : Entity<RespuestaId>
    {
        public ComentarioId RespondidoId { get; private set; }
        public ComentarioId RespuestaId { get; private set; }
        public RespuestaComentario(ComentarioId respondido, ComentarioId respuesta)
        {
            Id = new RespuestaId(Guid.NewGuid());
            RespondidoId = respondido;
            RespuestaId = respuesta;
        }
        public RespuestaComentario() { }
    }
}