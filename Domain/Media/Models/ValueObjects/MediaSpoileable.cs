using Domain.Core;

namespace Domain.Media.Models.ValueObjects {
    public class MediaSpoileableId : EntityId
    {
        public MediaSpoileableId(Guid value) : base(value) { }
    }
}

