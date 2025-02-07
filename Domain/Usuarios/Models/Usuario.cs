using Domain.Usuarios.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Usuarios.Models
{
    public class Usuario : IdentityUser<IdentityId> {
        public string? StaffName {get;set;}
        public DateTime RegistradoEn {get;set;}
    }

   
}