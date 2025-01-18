using Application.Medias.Abstractions.Providers;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Services.Providers
{
    public class UrlConfigurationProvider : IMediaUrlProvider
    {
        private readonly IConfiguration _configuration;
        public UrlConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BaseUrl => _configuration["Url"]!;
        public string Media => BaseUrl + "/media/";
        public string Files => BaseUrl + "/media/files/";

        public string Thumbnail => Media + "thumbnails/";
        public string Previsualizacion =>  Media + "previsualizaciones/";
    }
}