using Application.Core.Abstractions.Messaging;
using Application.Medias.Abstractions;
using Application.Medias.Abstractions.Providers;

namespace Application.Comentarios.Commands.ComentarHilo
{
    public class ComentarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public string Texto { get; set; }
        public IEmbedFileProvider? EmbedFile { get; set; }
        public IFileProvider? File { get; set; }
        public bool Spoiler {get; set;}

        public ComentarHiloCommand(Guid hilo, string texto, IEmbedFileProvider? embedFile, IFileProvider? file, bool spoiler)
        {
            Hilo = hilo;
            Texto = texto;
            EmbedFile = embedFile;
            File = file;
            Spoiler = spoiler;
        }

    }
}