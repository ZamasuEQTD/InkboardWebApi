using Domain.Core;

namespace Domain.Usuarios.Models.ValueObjects
{
     public class UsuarioId : EntityId, IEquatable<UsuarioId> {
        private UsuarioId(){}
        public UsuarioId(Guid id) : base(id) { }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? other)
        {
            return base.Equals(other);
        }
        public bool Equals(UsuarioId? other)
        {
            return base.Equals(other);
        }
    }
    
}