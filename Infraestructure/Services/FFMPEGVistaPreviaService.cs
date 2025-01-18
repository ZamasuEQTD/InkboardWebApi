using Application.Medias.Abstractions;

namespace Infraestructure.Services
{
    public class FfmpegVideoVistaPreviaService : IVideoGifPrevisualizadorService
    {
        static private readonly NReco.VideoConverter.FFMpegConverter FFmpeg = new NReco.VideoConverter.FFMpegConverter();

        public Stream Generar(string path)
        {
            Stream stream = new MemoryStream();

            FFmpeg.GetVideoThumbnail(path, stream);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}