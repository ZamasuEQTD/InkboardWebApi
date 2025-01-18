namespace Application.Medias.Abstractions
{
    public interface IVideoGifPrevisualizadorService
    {
        Stream Generar(string path);
    }
}