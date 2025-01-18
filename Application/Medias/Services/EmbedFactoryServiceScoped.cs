using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Medias.Services {
    
public class EmbedMediaFactoryServiceScoped : IEmbedServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    public EmbedMediaFactoryServiceScoped(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEmbedService Create(EmbedType type)
    {
        return type switch
        {
            EmbedType.Youtube => _serviceProvider.GetRequiredService<YoutubeEmbedService>(),
            _ => throw new NotImplementedException()
        };
    }
    }
}
