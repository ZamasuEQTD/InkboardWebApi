using Domain.Usuarios.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Usuarios.Models
{
    public class UsuarioRole :  IdentityRole<UsuarioId>
    {
        public string ShortName { get; set; }
    }
}