using Application.Core.Exceptions;

namespace Application.Auth
{
    public static class AuthExceptions
    {
        public static readonly Exception CreedencialesInvalidas = new InvalidCommandException("Creedenciales invalidas");
        public static readonly Exception UsuarioNoEncontrado = new InvalidCommandException("Usuario no encontrado");
        public static readonly Exception UsernameOcupado = new InvalidCommandException("Nombre de usuario ocupado");
        public static readonly Exception PasswordLongitudInvalida = new InvalidCommandException("La contraseña debe tener entre 8 y 16 caracteres.");
        public static readonly Exception PasswordContineneEspacios = new InvalidCommandException("La contraseña no debe tener espacios");
        public static readonly Exception UsernameLongitudInvalida = new InvalidCommandException("El nombre de usuario debe tener entre 5 y 8 caracteres.");
        public static readonly Exception UsernameContieneEspacios = new InvalidCommandException("El nombre de usuario no debe tener espacios");
    }    
}