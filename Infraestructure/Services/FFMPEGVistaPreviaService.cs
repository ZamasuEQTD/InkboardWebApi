using Application.Medias.Abstractions;
using FFMpegCore;
using FFMpegCore.Pipes;


namespace Infraestructure.Services
{
    public class FfmpegVideoVistaPreviaService : IVideoGifPrevisualizadorService
    {
        // static private readonly NReco.VideoConverter.FFMpegConverter FFmpeg = new NReco.VideoConverter.FFMpegConverter();

        public Stream Generar(string path)
        {
            Stream stream = new MemoryStream();

            FFMpegArguments.FromFileInput(path)
            .OutputToPipe(new StreamPipeSink(stream), options => options
            .Seek(TimeSpan.FromSeconds(0)) // Saltar al segundo 5
            .WithFrameOutputCount(1)       // Capturar solo 1 frame
            .ForceFormat("image2pipe")     // Especificar que la salida es un stream
            .WithVideoCodec("mjpeg")).ProcessSynchronously();

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
 
    }

}