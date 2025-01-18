
using Domain.Core.Abstractions;
using Domain.Media.Models.ValueObjects;

namespace Domain.Media.Models {
    
    public class MediaSpoileable : Entity<MediaSpoileableId>
    {   
        public HashedMediaId HashedMediaId { get; private set; }
        public HashedMedia HashedMedia { get; private set; }
        public bool Spoiler { get; private set; }

        public MediaSpoileable(HashedMediaId hashedMediaId, HashedMedia hashedMedia, bool spoiler) : base()
        {
            Id = new MediaSpoileableId(Guid.NewGuid());
            HashedMediaId = hashedMediaId;
            HashedMedia = hashedMedia;
            Spoiler = spoiler;
        }
        private MediaSpoileable() { }
    }
}
