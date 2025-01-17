using Domain.Core.Abstractions;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Hilos.Models
{
     public class HiloInteraccion : Entity<HiloInteraccionId>
    {
        public HiloId HiloId { get; private set; }
        public IdentityId UsuarioId { get; private set; }
        public bool Seguido { get; private set; }
        public bool Favorito { get; private set; }
        public bool Oculto { get; private set; }

        private HiloInteraccion() { }

        public HiloInteraccion(HiloId hiloId, IdentityId usuarioId)
        {
            this.Id = new(Guid.NewGuid());
            this.HiloId = hiloId;
            this.UsuarioId = usuarioId;
            this.Seguido = false;
            this.Favorito = false;
            this.Oculto = false;
        }

        internal void Seguir()
        {
            this.Seguido = !Seguido;
        }
        internal void Ocultar()
        {
            this.Oculto = !Oculto;
        }
        internal void PonerEnFavoritos()
        {
            this.Favorito = !Favorito;
        }

        public void EjecutarAccion(Acciones accion)
        {
            switch (accion)
            {
                case Acciones.Seguir:
                    Seguir();
                    break;
                case Acciones.Favorito:
                    PonerEnFavoritos();
                    break;
                case Acciones.Ocultar:
                    Ocultar();
                    break;
                default:
                    throw new ArgumentException("Accion invalida");
            }
        }

        public enum Acciones
        {
            Seguir,
            Ocultar,
            Favorito
        }
    }
}