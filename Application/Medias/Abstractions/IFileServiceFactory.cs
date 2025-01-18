using Application.Medias.Abstractions.Providers;
using Application.Medias.Abstractions.Services;

namespace Application.Medias.Abstractions
{
    
    public interface IFileServiceFactory
    {
        IFileService Create(FileType tipo);
    }
}