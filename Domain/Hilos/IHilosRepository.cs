using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;

namespace Domain.Hilos
{
    public interface IHilosRepository
    {
        void Add(Hilo hilo);
        Task<Hilo?> GetHiloById(HiloId id);
    }
}