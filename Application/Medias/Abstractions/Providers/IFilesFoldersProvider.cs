namespace Application.Medias.Abstractions.Providers
{
    public interface IMediaFolderProvider
    {
        string ThumbnailFolder { get; }
        string FilesFolder { get; }
        string Previsualizaciones { get; }
    }
}