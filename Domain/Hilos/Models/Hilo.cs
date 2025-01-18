using Domain.Categorias.Models.ValueObjects;
using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Denuncias.Models;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Hilos.Models.Enums;
using Domain.Hilos.Models.ValueObjects;
using Domain.Media.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class Hilo : Entity<HiloId>
    {
        public IdentityId AutorId {get; private set;}
        public Sticky? Sticky {get; private set;}
        public HiloStatus Status {get; private set;}
        public DateTime UltimoBump { get; private set; }
        public string Titulo {get; private set;}
        public string Descripcion {get;private set;}
        public bool RecibirNotificaciones { get; private set; }
        public ConfiguracionDeComentarios Configuracion {get; private set;}
        public MediaSpoileableId PortadaId {get; private set;}
        public SubcategoriaId SubcategoriaId {get; private set;}
        public EncuestaId? EncuestaId {get; private set;}
        public ICollection<DenunciaHilo> Denuncias {get; private set;} = [];
        public ICollection<HiloInteraccion> Interacciones {get; private set;} = [];
        public ICollection<Comentario> Comentarios {get; private set;} = [];
        public ICollection<ComentarioDestacado> ComentariosDestacados {get; private set;} = [];
        

        public void DestacarComentario(IdentityId usuario, Comentario comentario){
            if(!EstaActivo) throw new DomainBusinessException("Solo puedas destacar en hilos activos");

            if(comentario.EstaEliminado) throw new DomainBusinessException("Comentario Eliminado");

            if(!EsAutor(usuario)) throw new DomainBusinessException("Solamente el autor puede realizar esta accion");
        
            if(ComentarioEstaDestacado(comentario.Id)) {
                this.ComentariosDestacados = [.. ComentariosDestacados.Where(c => c.Id == comentario.Id)];
                
                return;
            }

            if(HaAlcandoMaximaCantidadDeDestacados) throw new DomainBusinessException("Haz alcanzado la maxima cantidad de comentarios destacados");


            this.ComentariosDestacados.Add(new ComentarioDestacado(comentario.Id,Id));
        }

        
        public void Eliminar() {
            if(EstaEliminado) throw new DomainBusinessException("Hilo ya eliminado");

            if(TieneStickyActivo) EliminarSticky(); 

            DesestimarDenuncias();

            this.Status = HiloStatus.Eliminado;
        }
    
        public void EliminarSticky() {
            if(!TieneStickyActivo) throw new DomainBusinessException("Sin sticky activo");
        
            this.Sticky = null;
        }

        public void EstablecerSticky(){
            if(TieneStickyActivo) throw new DomainBusinessException("Ya tiene un sticky activo");

            this.Sticky = new Sticky(this.Id);
        }

        public void Denunciar(IdentityId usuario)
        {
            if (HaDenunciado(usuario)) throw new DomainBusinessException("Ya has denunciado este hilo");

            Denuncias.Add(new DenunciaHilo(
                usuario,
                Id
            ));

        }

        private void DesestimarDenuncias(){
            foreach (var denuncia in Denuncias)
            {
                if(!denuncia.EstaDesestimada){
                    denuncia.Desestimar();
                }
            }
        }

        public void RealizarInteraccion(HiloInteraccion.Acciones accion, IdentityId usuario){

            if(EstaEliminado) throw new DomainBusinessException("No puedes interactuar con un hilo eliminado");

            HiloInteraccion? interaccion = GetInteraccionDeUsuario(usuario);
        
            if(interaccion is null) {

                interaccion = new HiloInteraccion(Id,usuario);

                this.Interacciones.Add(interaccion);
            }

            interaccion.EjecutarAccion(accion);
        }

        public void CambiarSubcategoria (SubcategoriaId subcategoria){
            this.SubcategoriaId = subcategoria;
        }

        public HiloInteraccion? GetInteraccionDeUsuario(IdentityId usuario) => this.Interacciones.FirstOrDefault(i => i.UsuarioId == usuario); 

        bool HaAlcandoMaximaCantidadDeDestacados => ComentariosDestacados.Count == 5;
        public bool TieneStickyActivo => this.Sticky is not null;
        public bool EstaEliminado => this.Status == HiloStatus.Eliminado;
        public bool EstaActivo => this.Status == HiloStatus.Activo;
        public bool ComentarioEstaDestacado(ComentarioId comentarioId) => this.ComentariosDestacados.Any(d=> d.ComentarioId == comentarioId);
        public bool EsAutor(IdentityId usuarioId) => this.AutorId == usuarioId; 
        public bool HaDenunciado(IdentityId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);
    }
}