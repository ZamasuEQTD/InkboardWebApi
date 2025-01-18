using Application.Core.Abstractions;
using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Domain.Media;
using Domain.Media.Models;

namespace Application.Medias.Services { 
    
    public class EmbedProcesador
    {
        private readonly IHasher _hasher;
        private readonly IMediasRepository _repository;
        private readonly IEmbedServiceFactory _embedFactory;
        public EmbedProcesador(IHasher hasher, IMediasRepository repository, IEmbedServiceFactory embedFactory)
        {
            _hasher = hasher;
            _repository = repository;
            _embedFactory = embedFactory;
        }
    
        public async Task<HashedMedia> Procesar(IEmbedFileProvider file)
        {
            var hash = await _hasher.Hash(file.Url);
        
            HashedMedia? media = await _repository.GetHashedMediaByHash(hash);
    
            if (media is not null) return media;
    
            return new HashedMedia(
                null, 
                hash,
                file.Url,
                await _embedFactory.Create(file.Type).Create(file)
            );
        }
    }
}