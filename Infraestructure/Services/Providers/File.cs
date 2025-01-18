using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Services.Providers
{
    public class FormFileImplementation(IFormFile file) : IFileProvider
    {
        private static readonly Dictionary<string, FileType> types = new Dictionary<string, FileType>(){
            { "image", FileType.Imagen},
            { "video", FileType.Video},
        };
        public string FileName => file.FileName;
        public Stream Stream => file.OpenReadStream();
        public string ContentType => file.ContentType;
        public string Extension => Path.GetExtension(FileName);
        public FileType Type => GetFileType();
        private string SingleType => ContentType.Split("/")[0];
        private FileType GetFileType()
        {
            if (ContentType.Contains("gif")) return FileType.Gif;

            if (types.TryGetValue(SingleType, out var type)) return type;

            return FileType.Desconocido;
        }
    }
}