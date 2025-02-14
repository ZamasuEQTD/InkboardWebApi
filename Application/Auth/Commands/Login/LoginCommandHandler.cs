using Application.Core.Abstractions.Messaging;
using Application.Auth.Commands.Login;
using Domain.Core;
using Domain.Usuarios;
using Domain.Usuarios.Models;
using Microsoft.AspNetCore.Identity;
using Application.Core.Abstractions;

namespace Application.Auth.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(UserManager<Usuario> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _userManager.FindByNameAsync(request.Username);

            if(usuario is null ) return UsuarioErrors.CredencialesInvalidas;

            PasswordHasher<Usuario> hasher = new  PasswordHasher<Usuario>();

            if(hasher.VerifyHashedPassword(usuario, usuario.PasswordHash!, request.Password) == PasswordVerificationResult.Success) return UsuarioErrors.CredencialesInvalidas;
                
            return  await _jwtProvider.Generar(usuario);
        }

    }
}