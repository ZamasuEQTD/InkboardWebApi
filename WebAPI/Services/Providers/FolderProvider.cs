using Application.Medias.Abstractions.Providers;

namespace Infraestructure.Services.Providers
{
    public class FolderProvider(IWebHostEnvironment environment) : IMediaFolderProvider
    {
        public string ThumbnailFolder => Path.Join(BaseMediaFolder, "thumbnails");
        public string FilesFolder => Path.Join(BaseMediaFolder, "files");
        public string Previsualizaciones => Path.Join(BaseMediaFolder, "previsualizaciones");
        private string BaseMediaFolder => Path.Join(environment.ContentRootPath, "media");
    }
}