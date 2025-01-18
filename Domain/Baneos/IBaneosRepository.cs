using Domain.Baneos.Models.ValueObjects;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Baneos
{
    public interface IBaneosRepository
    {
        void Add(Baneo baneo);
        Task<Baneo> GetBaneoById(BaneoId id);
        Task<List<Baneo>> GetBaneos(IdentityId usuarioId);
    }
}