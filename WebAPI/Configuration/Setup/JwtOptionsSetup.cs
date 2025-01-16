using Infraestructure.Authentication.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace WebAPI.Configuration.Setup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration;
        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            IConfigurationSection section = _configuration.GetSection(SectionName);
            section.Bind(options);
        }
    }
}