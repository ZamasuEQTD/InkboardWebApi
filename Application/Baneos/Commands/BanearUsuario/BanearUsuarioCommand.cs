using Application.Core.Abstractions.Messaging;
using Domain.Baneos;
using Domain.Baneos.Models.Enums;

namespace Application.Baneos.Commands.BanearUsuario
{
    public class BanearUsuarioCommand : ICommand
    {
        public Guid UsuarioId { get; set; }
        public string? Mensaje { get; set; }
        public BaneoRazon Razon { get; set; }
        public DuracionBaneo? Duracion   { get; set; }
    }
}