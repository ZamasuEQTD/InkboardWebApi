using Domain.Usuarios;
using Domain.Usuarios.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Abstractions
{
    public interface IJwtProvider
    {
        public Task<string> Generar(Usuario usuario);
    }
}