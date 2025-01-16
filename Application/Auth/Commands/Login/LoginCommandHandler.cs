using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Auth.Commands.Login;
using Domain.Core;
using Domain.Usuarios;
using Domain.Usuarios.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands
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

            if(usuario is null ) return UsuariosFailures.CredencialesInvalidas;

            PasswordHasher<Usuario> hasher = new  PasswordHasher<Usuario>();

            if(hasher.VerifyHashedPassword(usuario, usuario.PasswordHash!, request.Password) == PasswordVerificationResult.Success) return UsuariosFailures.CredencialesInvalidas;
                
            return  await _jwtProvider.Generar(usuario);
        }

    }
}