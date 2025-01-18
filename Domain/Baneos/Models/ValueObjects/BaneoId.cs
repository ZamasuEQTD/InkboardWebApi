using Domain.Core;

namespace Domain.Baneos.Models.ValueObjects
{
    public class BaneoId : EntityId
    {
        public BaneoId(Guid id) : base(id) { }
    }
}