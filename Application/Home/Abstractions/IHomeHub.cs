using Application.Hilos.Queries.GetPortadas;

namespace Application.Home.Abstractions
{
     public interface IHomeHub {
        Task NotificarHiloPosteado(GetPortadaResponse portada);
        Task NotificarHiloEliminado(Guid hiloId);
    }
}