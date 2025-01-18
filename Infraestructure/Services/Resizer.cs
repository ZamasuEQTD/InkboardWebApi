using Application.Medias.Abstractions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Infraestructure.Media
{
    public class Resizer : IImageResizer
    {
        public async Task<Stream> Resize(string path, int width, int height)
        {
            var image = await Image.LoadAsync(path);

            var resized = image.Clone(i => i.Resize(
                new ResizeOptions()
                {
                    Mode = ResizeMode.Crop,
                    Position = AnchorPositionMode.Center,
                    Size = new Size(
                        width,
                        height
                    )
                }
            ));

            Stream stream = new MemoryStream();

            await resized.SaveAsync(stream, new JpegEncoder());

            return stream;
        }

        public async Task<Stream> Resize(Stream stream, int width, int height)
        {
            var image = await Image.LoadAsync(stream);

            var resized = image.Clone(i => i.Resize(
                new ResizeOptions()
                {
                    Mode = ResizeMode.Crop,
                    Position = AnchorPositionMode.Center,
                    Size = new Size(
                        width,
                        height
                    )
                }
            ));

            Stream resizedStream = new MemoryStream();

            await resized.SaveAsync(resizedStream, new JpegEncoder());

            return resizedStream;
        }
    }
}