using Domain.Comentarios.Models.ValueObjects;
using Domain.Core.Abstractions;
using Domain.Hilos.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class ComentarioDestacado : Entity<ComentarioDestacadoId>
    {
        public ComentarioId ComentarioId { get; private set; }
        public HiloId HiloId { get; private set; }

        public ComentarioDestacado(ComentarioId comentarioId, HiloId hiloId)
        {
            Id = new ComentarioDestacadoId(Guid.NewGuid());
            ComentarioId = comentarioId;
            HiloId = hiloId;
        }
    }
}