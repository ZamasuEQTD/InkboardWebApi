using Domain.Core;

namespace Domain.Hilos.Models.ValueObjects
{
     public class ComentarioDestacadoId : EntityId
    {
        public ComentarioDestacadoId(Guid id) : base(id) { }

        private ComentarioDestacadoId() {}
    }
}