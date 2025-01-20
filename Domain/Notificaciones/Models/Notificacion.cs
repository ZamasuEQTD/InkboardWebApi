using Domain.Comentarios.Models.ValueObjects;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Hilos.Models.ValueObjects;
using Domain.Notificaciones.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Notificaciones
{
    public abstract class Notificacion : Entity<NotificacionId>
    {
        public IdentityId NotificadoId { get; private set; }
        public bool Leida { get; private set; }
        public HiloId HiloId { get; private set; }
        public ComentarioId ComentarioId { get; private set; }

        protected Notificacion() { }
        protected Notificacion(IdentityId IdentityId, HiloId hiloId, ComentarioId comentarioId)
        {
            this.Id = new(Guid.NewGuid());
            this.NotificadoId = IdentityId;
            this.Leida = false;
            HiloId = hiloId;
            ComentarioId = comentarioId;
        }

        public Result Leer(IdentityId IdentityId)
        {
            if (IdentityId != NotificadoId) return NotificacionesFailures.NoTePertenece;

            this.Leida = true;

            return Result.Success();
        }

        public bool EsUsuarioNotificado(IdentityId IdentityId) => this.NotificadoId == IdentityId;
    }   

    static public class NotificacionesFailures
    {
        public readonly static Error NoTePertenece = new Error("notificacion.no_te_pertenece", "No te pertenece esta notificación");
    }

    public class HiloComentadoNotificacion : Notificacion
    {
        private HiloComentadoNotificacion() { }
        public HiloComentadoNotificacion(IdentityId IdentityId, HiloId hilo, ComentarioId comentarioId) : base(IdentityId, hilo, comentarioId) { }
    }

    public class HiloSeguidoNotificacion : Notificacion
    {
        private HiloSeguidoNotificacion() { }
        public HiloSeguidoNotificacion(IdentityId IdentityId, HiloId hilo, ComentarioId comentarioId) : base(IdentityId, hilo, comentarioId) { }
    }
    public class ComentarioRespondidoNotificacion : Notificacion
    {
        public ComentarioId RespondidoId { get; private set; }
        private ComentarioRespondidoNotificacion() { }
        public ComentarioRespondidoNotificacion(IdentityId IdentityId, HiloId hilo, ComentarioId comentarioId, ComentarioId respondidoId) : base(IdentityId, hilo, comentarioId)
        {
            RespondidoId = respondidoId;
        }
    }
}