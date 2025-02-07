using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
using Domain.Core;
using Domain.Usuarios;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;
using Domain.Utils;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands.Registro
{
    public class RegistroCommandHandler : ICommandHandler<RegistroCommand, string>
    {

        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<Usuario> _userManager;

        public RegistroCommandHandler(UserManager<Usuario> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(RegistroCommand request, CancellationToken cancellationToken)
        {
            
            var username = Username.Create(request.Username);

            if(username.IsFailure) return username.Error;

            var password = Password.Create(request.Password);

            if(password.IsFailure) return password.Error;

            Usuario? usuario = await  _userManager.FindByNameAsync(username.Value.Value);
            
            if (usuario is not null) return UsuarioErrors.UsernameOcupado;

            PasswordHasher<Usuario> hasher = new PasswordHasher<Usuario>();

            usuario = new Usuario (){
                Id = new IdentityId(Guid.NewGuid()),
                UserName = request.Username
            };

            usuario.PasswordHash = hasher.HashPassword(usuario, request.Password);

            await _userManager.CreateAsync(usuario);
        
            await _userManager.AddToRoleAsync(usuario, AppRoles.Anonimo);

            return await _jwtProvider.Generar(usuario);
        }
    }
}