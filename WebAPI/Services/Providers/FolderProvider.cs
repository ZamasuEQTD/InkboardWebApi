using Application.Medias.Abstractions.Providers;

namespace Infraestructure.Services.Providers
{
    public class FolderProvider(IWebHostEnvironment environment) : IMediaFolderProvider
    {
        public string ThumbnailFolder => Path.Join(BaseMediaFolder, "Thumbnails");
        public string FilesFolder => Path.Join(BaseMediaFolder, "Files");
        public string Previsualizaciones => Path.Join(BaseMediaFolder, "Previsualizaciones");
        private string BaseMediaFolder => Path.Join(environment.ContentRootPath, "Media");
    }
}