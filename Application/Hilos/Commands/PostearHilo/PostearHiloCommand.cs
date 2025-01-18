using Application.Core.Abstractions.Messaging;
using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;

namespace Application.Hilos.Commands.PostearHilo
{
    public class PostearHiloCommand : ICommand<Guid>
    {
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required bool Spoiler { get; set; }
        public required Guid Subcategoria { get; set; }
        public List<string> Encuesta { get; set; } = [];
        public required bool DadosActivados { get; set; }
        public required bool IdUnicoAtivado { get; set; }
        public required IFileProvider? File { get; set; }
        public required IEmbedFileProvider? Embed { get; set; }
    }
}