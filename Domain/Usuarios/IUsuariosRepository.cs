using Domain.Usuarios;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Usuarios
{
    public interface IUsuariosRepository
    {
        public Task<Usuario?> GetUsuarioById(IdentityId id);
    }
}