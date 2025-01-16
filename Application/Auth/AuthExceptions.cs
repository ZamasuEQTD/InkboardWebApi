using Application.Core.Exceptions;

namespace Application.Auth
{
    public static class AuthExceptions
    {
        public static readonly Exception CreedencialesInvalidas = new InvalidCommandException("Creedenciales invalidas");
        public static readonly Exception UsuarioNoEncontrado = new InvalidCommandException("Usuario no encontrado");
    }    
}