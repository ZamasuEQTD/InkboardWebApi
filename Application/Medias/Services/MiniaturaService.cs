using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;

namespace Application.Medias.Services { 
    
public class MiniaturaService
{
    private readonly IImageResizer _resizer;
    private readonly IMediaFolderProvider _folderProvider;
    private readonly IFileStorageService _fileStorage;
    public MiniaturaService(IImageResizer resizer, IMediaFolderProvider folderProvider, IFileStorageService fileStorage)
    {
        _resizer = resizer;
        _folderProvider = folderProvider;
        _fileStorage = fileStorage;
    }

    public async Task<string> Procesar(string imagen)
        {
            string miniatura_path = _folderProvider.ThumbnailFolder + "/" + Path.GetFileNameWithoutExtension(imagen) + ".jpeg";

            using Stream resized = await _resizer.Resize(
                imagen,
                200,
                200
            );

            await _fileStorage.GuardarArchivo(resized, miniatura_path);

            return miniatura_path;
        }

        public async Task<string> Procesar(Stream stream, string hash)
        {
            string miniatura_path = _folderProvider.ThumbnailFolder + "/" + hash + ".jpeg";

            using Stream resized = await _resizer.Resize(
                stream,
                200,
                200
            );

            await _fileStorage.GuardarArchivo(resized, miniatura_path);

            return miniatura_path;
        }
}
}