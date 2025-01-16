using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Infraestructure.Authentication.Jwt;

namespace WebAPI.Configuration.Setup {

    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {

        private readonly JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public void Configure(JwtBearerOptions options)
        {
            
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
           options.TokenValidationParameters =new(){
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };
        }
    }
}
