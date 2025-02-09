using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Application.Core.Abstractions;
using Microsoft.AspNetCore.Identity;
using Domain.Usuarios.Models;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.Authentication.Jwt
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly UserManager<Usuario> userManager;
        public JwtProvider(IOptions<JwtOptions> options, UserManager<Usuario> userManager)
        {
            _options = options.Value;
            this.userManager = userManager;
        }
        public async Task<string> Generar(Usuario usuario)
        {
            Claim[] claims = {
                new (JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new (JwtRegisteredClaimNames.Name,  usuario.StaffName ?? usuario.UserName!),
            };

            var roles =await  this.userManager.GetRolesAsync(usuario);

            claims = [.. claims, new Claim("role", JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray)];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(5),
                signingCredentials
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}