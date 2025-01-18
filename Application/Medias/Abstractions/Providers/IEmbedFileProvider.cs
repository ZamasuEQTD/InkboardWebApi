namespace Application.Medias.Abstractions.Providers
{
    public interface IEmbedFileProvider  { 
        public string Url { get; }
        public string Domain { get; }
        public EmbedType Type { get; }
    }
    public enum EmbedType {
        Youtube,
        Desconocido
    }

}