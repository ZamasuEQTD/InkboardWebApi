using Application.Core.Abstractions.Messaging;

namespace Application.Hilos.Commands.PonerHiloEnFavorito;

public class PonerHiloEnFavoritoCommand : ICommand
{
    public Guid Hilo { get; set; }
}