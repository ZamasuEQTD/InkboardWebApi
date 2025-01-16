using Domain.Core.Abstractions;
using Domain.Hilos.Models.Enums;
using Domain.Hilos.Models.ValueObject;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class Hilo : Entity<HiloId>
    {
        public IdentityId AutorId {get; private set;}
        public HiloStatus Status {get; private set;}
        public ConfiguracionDeComentarios Configuracion {get; private set;}
        public DateTime UltimoBump { get; private set; }
        public string Titulo {get; private set;}
        public string Descripcion {get;private set;}
        public bool RecibirNotificaciones { get; private set; }
    }
}