using Domain.Core.Abstractions;
using Domain.Denuncias.Models.Enums;
using Domain.Denuncias.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Denuncias.Models
{
    public abstract class Denuncia : Entity<DenunciaId>
    {
        public IdentityId DenuncianteId { get; private set; }
        public DenunciaStatus Status { get; private set; }

        protected Denuncia() { }
        protected Denuncia(IdentityId denuncianteId)
        {
            this.Id = new(Guid.NewGuid());
            this.Status = DenunciaStatus.Activa;
            this.DenuncianteId = denuncianteId;
        }

        public void Desestimar()
        {
            this.Status = DenunciaStatus.Destimada;
        }
        public bool EstaDesestimada => Status == DenunciaStatus.Destimada;
    }
}