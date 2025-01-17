using Domain.Core;

namespace Domain.Denuncias.Models.ValueObjects
{
   public class DenunciaId : EntityId
    {
        private DenunciaId() : base() { }

        public DenunciaId(Guid id) : base(id) { }
    }
}