
using Domain.Core;

namespace Domain.Media.Models.ValueObjects {
    public class HashedMediaId : EntityId
    {
        public HashedMediaId(Guid value) : base(value) { }
    }
}
