using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Denuncias.Models;
using Domain.Hilos.Models.Enums;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class Hilo : Entity<HiloId>
    {
        public IdentityId AutorId {get; private set;}
        public Sticky? Sticky {get; private set;}
        public HiloStatus Status {get; private set;}
        public ConfiguracionDeComentarios Configuracion {get; private set;}
        public DateTime UltimoBump { get; private set; }
        public string Titulo {get; private set;}
        public string Descripcion {get;private set;}
        public ICollection<DenunciaHilo> Denuncias {get; private set;}
        public bool RecibirNotificaciones { get; private set; }
        public void Eliminar() {
            if(Status == HiloStatus.Eliminado) throw new DomainBusinessException("Hilo ya eliminado");

            if(TieneStickyActivo) EliminarSticky(); 

            DesestimarDenuncias();

            this.Status = HiloStatus.Eliminado;
        }
    
        public void EliminarSticky() {
            if(!TieneStickyActivo) throw new DomainBusinessException("Sin sticky activo");
        
            this.Sticky = null;
        }

        private void DesestimarDenuncias(){
            foreach (var denuncia in Denuncias)
            {
                if(!denuncia.EstaDesestimada){
                    denuncia.Desestimar();
                }
            }
        }

        public bool TieneStickyActivo => this.Sticky is not null;

    }
}