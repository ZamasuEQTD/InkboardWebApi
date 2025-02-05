
using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Media.Models.ValueObjects;

namespace Domain.Media.Models
{
    public class HashedMedia : Entity<HashedMediaId>
    {
        public string? Filename { get; private set; }
        public string Hash { get; private set; }
        public string Url { get; private set; }
        public Media Media { get; private set; }
        private HashedMedia() { }

        public HashedMedia(string? filename, string hash, string url, Media media)
        {
            Id = new HashedMediaId(Guid.NewGuid());
            Filename = filename;
            Hash = hash;
            Url = url;
            Media = media;
        }
    }

    public class Media
    {
        public MediaProvider Provider { get; private set; }
        public string? Miniatura { get; private set; }
        public string? Previsualizacion { get; private set; }

        public Media(MediaProvider provider, string? miniatura, string? previsualizacion)
        {
            Provider = provider;
            Miniatura = miniatura;
            Previsualizacion = previsualizacion;
        }
        private Media() { }

    }
   
    public class MediaProvider : ValueObject
    {
        private MediaProvider() { }
        private MediaProvider(string value) => Value = value;
        public string Value { get; private set; }
        public static readonly MediaProvider Youtube = new MediaProvider("youtube");
        public static readonly MediaProvider Imagen = new MediaProvider("image");
        public static readonly MediaProvider Video = new MediaProvider("video");
        public static readonly MediaProvider Gif = new MediaProvider("gif");
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}