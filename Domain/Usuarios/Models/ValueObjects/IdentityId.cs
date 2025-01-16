using Domain.Core;

namespace Domain.Usuarios.Models.ValueObjects
{
     public class IdentityId : EntityId, IEquatable<IdentityId> {
        private IdentityId(){}
        public IdentityId(Guid id) : base(id) { }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? other)
        {
            return base.Equals(other);
        }
        public bool Equals(IdentityId? other)
        {
            return base.Equals(other);
        }
    }
    
}