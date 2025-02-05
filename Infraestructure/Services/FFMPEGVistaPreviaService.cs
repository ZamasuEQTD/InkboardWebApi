using Application.Medias.Abstractions;
using FFMpegCore;
using FFMpegCore.Pipes;


namespace Infraestructure.Services
{
    public class FfmpegVideoVistaPreviaService : IVideoGifPrevisualizadorService
    {
        public Stream Generar(string path)
        {
            Stream stream = new MemoryStream();

            FFMpegArguments.FromFileInput(path)
            .OutputToPipe(
                new StreamPipeSink(stream),
                    options => options
                    .Seek(TimeSpan.FromSeconds(0))
                    .WithFrameOutputCount(1)      
                    .ForceFormat("image2pipe")
                    .WithVideoCodec("mjpeg")
            )
            .ProcessSynchronously();

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
 
    }

}