using Domain.Usuarios.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Usuarios.Models
{
    public class Usuario : IdentityUser<UsuarioId> {
        public string? Moderador {get;set;}
        public DateTime RegistradoEn {get;set;}
    }
}