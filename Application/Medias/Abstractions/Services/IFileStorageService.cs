namespace Application.Medias.Services
{
    public interface IFileStorageService
    {
        Task GuardarArchivo(Stream stream, string path);
    }
}