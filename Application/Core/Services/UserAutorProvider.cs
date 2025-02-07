using Application.Core.Abstractions;
using Domain.Core;
using Domain.Core.Models;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Application.Core.Services
{
    public class UserAutorProvider
    {

        private readonly ICurrentUser _user;

        public UserAutorProvider(ICurrentUser user)
        {
            _user = user;
        }

        public AutorRole GetAutorRole(){
            var username = _user.Roles.Any(AppRoles.StaffRoles.Contains) ? _user.Username! : "anonimo";
            
            var role = _user.Roles.FirstOrDefault(AppRoles.StaffRoles.Contains) ?? "Anonimo";

            return new AutorRole(username, role);
        }
    }
}