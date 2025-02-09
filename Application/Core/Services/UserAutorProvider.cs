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

            var staffRole = AppRoles.StaffRoles.FirstOrDefault(_user.Roles.Contains);

            var username = staffRole is not null ? _user.Username! : "anonimo";
            
            var role = staffRole ?? AppRoles.Anonimo;

            return new AutorRole(username, role);
        }
    }
}