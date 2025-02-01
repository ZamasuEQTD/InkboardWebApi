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
        public string Media => BaseUrl + "/Media/";
        public string Files => BaseUrl + "/Media/Files/";

        public string Thumbnail => Media + "Thumbnails/";
        public string Previsualizacion =>  Media + "Previsualizaciones/";
    }
}