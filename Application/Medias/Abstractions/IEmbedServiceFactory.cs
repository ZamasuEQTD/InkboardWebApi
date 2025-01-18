using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;

namespace Application.Medias.Abstractions
{
    public interface IEmbedServiceFactory
    {
        IEmbedService Create(EmbedType file);
    }
}