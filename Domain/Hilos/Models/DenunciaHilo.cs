using Domain.Denuncias.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class DenunciaHilo : Denuncia
    {
        public HiloId HiloId { get; private set; }
        public RazonDeDenuncia Razon { get; private set; }
        public DenunciaHilo(IdentityId autorId, HiloId hiloId) : base(autorId)
        {
            this.HiloId = hiloId;
            this.Razon = RazonDeDenuncia.Otro;
        }

        private DenunciaHilo() : base() { }

        public enum RazonDeDenuncia
        {
            Otro
        }
    }
}