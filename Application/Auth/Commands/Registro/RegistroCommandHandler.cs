using Application.Core.Abstractions;
using Application.Core.Abstractions.Messaging;
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

        public async Task<string> Handle(RegistroCommand request, CancellationToken cancellationToken)
        {
            
            if(request.Username.Length < 8 || request.Username.Length > 16)  throw AuthExceptions.UsernameLongitudInvalida;    

            if(StringUtils.ContieneEspaciosEnBlanco(request.Username)) throw AuthExceptions.UsernameContieneEspacios;

            if(request.Password.Length < 8 || request.Password.Length > 16) throw AuthExceptions.PasswordLongitudInvalida;
        
            if(StringUtils.ContieneEspaciosEnBlanco(request.Password)) throw   AuthExceptions.PasswordContineneEspacios;

            Usuario? usuario = await  _userManager.FindByNameAsync(request.Username);
            
            if (usuario is not null) throw AuthExceptions.UsernameOcupado;

            PasswordHasher<Usuario> hasher = new PasswordHasher<Usuario>();

            usuario = new Usuario (){
                Id = new IdentityId(Guid.NewGuid()),
                UserName = request.Username
            };

            usuario.PasswordHash = hasher.HashPassword(usuario, request.Password);

            await _userManager.CreateAsync(usuario);
        
            await _userManager.AddToRoleAsync(usuario, "Anonimo");

            return await _jwtProvider.Generar(usuario);
        }
    }
}