using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;

namespace Infraestructure.Media
{
    public class EmbedFile : IEmbedFileProvider
    {
        static private readonly Dictionary<string, EmbedType> _domains =  new Dictionary<string, EmbedType> {
            { "www.youtube.com", EmbedType.Youtube },
        };

        private readonly string _url;
        public string Url => _url;

        public string Domain =>  Uri.TryCreate(_url, UriKind.Absolute, out var uri) ? uri.Host : "";

        public EmbedType Type => _domains.GetValueOrDefault(Domain, EmbedType.Desconocido);

        public EmbedFile(string path)
        {
            _url = path;
        }
    }
}