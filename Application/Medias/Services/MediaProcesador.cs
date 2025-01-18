
using Application.Core.Abstractions;
using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;
using Domain.Media;
using Domain.Media.Models;

namespace Application.Medias.Services;

public class MediaProcesador {
    private readonly IHasher _hasher;
    private readonly IFileStorageService _fileStorage;
    private readonly IMediasRepository _repository;
    private readonly IMediaFolderProvider _folderProvider;
    private readonly IFileServiceFactory _mediaFactory;
    private readonly IMediaUrlProvider _url;

    public MediaProcesador(IHasher hasher, IFileStorageService fileStorage, IMediasRepository repository, IMediaFolderProvider folderProvider, IFileServiceFactory mediaFactory, IMediaUrlProvider url)
    {
        _hasher = hasher;
        _fileStorage = fileStorage;
        _repository = repository;
        _folderProvider = folderProvider;
        _mediaFactory = mediaFactory;
        _url = url;
    }

    public async Task<HashedMedia> Procesar(IFileProvider file)
    {
        Stream stream = file.Stream;

        string hash = await _hasher.Hash(stream);

        HashedMedia? media = await _repository.GetHashedMediaByHash(hash);

        if (media is not null) return media;

        string media_path = _folderProvider.FilesFolder + "/" + Guid.NewGuid() + file.Extension;

        await _fileStorage.GuardarArchivo(stream, media_path);

        return new HashedMedia(
            file.FileName,
            hash,
            _url.Files + Path.GetFileName(media_path),
            await _mediaFactory.Create(file.Type).Create(media_path)
        );
    }
}
