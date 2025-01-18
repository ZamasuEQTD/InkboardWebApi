using Domain.Baneos.Models.Enums;
using Domain.Baneos.Models.ValueObjects;
using Domain.Core.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Baneos
{
    public class Baneo : Entity<BaneoId>
    {
        public IdentityId ModeradorId { get; private set; }
        public IdentityId UsuarioBaneadoId { get; private set; }
        public DateTime? Concluye { get; private set; }
        public BaneoRazon Razon { get; private set; }
        public string? Mensaje { get; private set; }
        public bool Activo(DateTime utcNow) => Concluye is not null && utcNow < Concluye;

        public Baneo(IdentityId moderadorId, IdentityId usuarioBaneadoId, DateTime? concluye, string? mensaje, BaneoRazon razon)
        {
            this.Id = new BaneoId(Guid.NewGuid());
            this.ModeradorId = moderadorId;
            this.UsuarioBaneadoId = usuarioBaneadoId;
            this.Concluye = concluye;
            this.Mensaje = mensaje;
            this.Razon = razon;
        }

        public void Eliminar(DateTime now)
        {
            Concluye = now;
        }
    }
    
}