using System.Security.Claims;
using Application.Core.Abstractions;
using Domain.Core;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Authentication
{
    public class CurrentUser (IHttpContextAccessor context): ICurrentUser
    {
       public bool IsAuthenticated =>
        context
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("No hay current user disponible");

        public Guid UsuarioId => Guid.Parse(context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)!.Value);
        public string Username =>   context.HttpContext!.User.Identity!.Name!;
        public List<string> Roles => context.HttpContext!.User.Claims.Where( s=> s.Type == "role").Select(c=> c.Value).ToList() ;
        public bool EsModerador => Roles.Contains(AppRoles.Moderador);
    }
}