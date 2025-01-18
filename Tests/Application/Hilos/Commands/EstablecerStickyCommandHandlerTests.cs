using Application.Hilos.Commands.EstablecerSticky;
using Application.Core.Abstractions.Messaging;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;
using NSubstitute;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Media.Models.ValueObjects;
using Domain.Categorias.Models.ValueObjects;
using Domain.Hilos;
using Application.Core.Abstractions;
using Application.Core.Exceptions;

namespace Tests.Application.Hilos.Commands
{
    public class EstablecerStickyCommandHandlerTests
    {
        private readonly ICommandHandler<EstablecerStickyCommand> _commandHandler;
        private readonly IHilosRepository _hiloRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EstablecerStickyCommandHandlerTests()
        {
            _hiloRepository = Substitute.For<IHilosRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _commandHandler = new EstablecerStickyCommandHandler(_unitOfWork, _hiloRepository);
        }

        [Fact]
        public async Task Handle_HiloExistente_DeberiaEstablecerSticky()
        {
            // Arrange
            var hiloId = new HiloId(Guid.NewGuid());
            var command = new EstablecerStickyCommand { Hilo = hiloId.Value };
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );

            _hiloRepository.GetHiloById(Arg.Any<HiloId>())
                .Returns(hilo);

            // Act
            await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(hilo.TieneStickyActivo);
            await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_HiloNoExistente_DeberiaLanzarExcepcion()
        {
            // Arrange
            var hiloId = new HiloId(Guid.NewGuid());
            var command = new EstablecerStickyCommand { Hilo = hiloId.Value };

            _hiloRepository.GetHiloById(Arg.Any<HiloId>()).Returns((Hilo?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCommandException>(() => _commandHandler.Handle(command, CancellationToken.None));
        }
    }
}