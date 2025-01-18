namespace Application.Medias.Abstractions.Providers
{
    public interface IMediaUrlProvider {
        string BaseUrl {get;}
        string Media {get;}
        string Files {get;}
        string Thumbnail {get;}
        string Previsualizacion {get;}
    }
}