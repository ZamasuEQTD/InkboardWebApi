namespace Application.Medias.Abstractions
{
    public interface IImageResizer
    {
        Task<Stream> Resize(string path, int width, int height);
        Task<Stream> Resize(Stream image, int width, int height);
    }
}