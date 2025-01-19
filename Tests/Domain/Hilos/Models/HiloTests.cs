using Domain.Hilos.Models;
using Domain.Hilos.Models.Enums;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models.ValueObjects;
using Xunit;
using System;
using Domain.Categorias.Models.ValueObjects;
using Domain.Media.Models.ValueObjects;
using Domain.Core;

namespace Tests.Domain.Hilos.Models
{
    public class HiloTests
    {
        [Fact]
        public void Eliminar_HiloActivo_DeberiaCambiarEstadoAEliminado()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );

            // Act
            hilo.Eliminar();

            // Assert
            Assert.True(hilo.EstaEliminado);
        }

        [Fact]
        public void Eliminar_HiloYaEliminado_DeberiaLanzarExcepcion()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)

            );
            hilo.Eliminar();

            // Act
            var result = hilo.Eliminar();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HiloErrors.HiloEliminado, result.Error);
        }

        [Fact]
        public void Eliminar_HiloConSticky_DeberiaEliminarSticky()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)

            );
            hilo.EstablecerSticky();

            // Act
            hilo.Eliminar();

            // Assert
            Assert.False(hilo.TieneStickyActivo);
        }

        [Fact]
        public void EstablecerSticky_HiloSinSticky_DeberiaEstablecerSticky()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );

            // Act
            hilo.EstablecerSticky();

            // Assert
            Assert.True(hilo.TieneStickyActivo);
        }

        [Fact]
        public void EstablecerSticky_HiloConSticky_DeberiaLanzarExcepcion()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );
            hilo.EstablecerSticky();

            // Act
            var result = hilo.EstablecerSticky();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HiloErrors.StickyActivo, result.Error);
        }


        [Fact]
        public void Denunciar_HiloValido_DeberiaAgregarDenuncia()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );
            var usuarioId = new IdentityId(Guid.NewGuid());

            // Act
            hilo.Denunciar(usuarioId);

            // Assert
            Assert.Contains(hilo.Denuncias, d => d.DenuncianteId == usuarioId);
        }

        [Fact]
        public void RealizarInteraccion_InteraccionValida_DeberiaAgregarInteraccion()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );
            var usuarioId = new IdentityId(Guid.NewGuid());

            // Act
            hilo.RealizarInteraccion(HiloInteraccion.Acciones.Ocultar, usuarioId);

            // Assert
            Assert.Contains(hilo.Interacciones, i => i.UsuarioId == usuarioId);
        }

        [Fact]
        public void CambiarSubcategoria_SubcategoriaValida_DeberiaCambiarSubcategoria()
        {
            // Arrange
            var hilo = new Hilo(
                new IdentityId(Guid.NewGuid()),
                "Titulo",
                "Descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                new MediaSpoileableId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );
            var nuevaSubcategoria = new SubcategoriaId(Guid.NewGuid());

            // Act
            hilo.CambiarSubcategoria(nuevaSubcategoria);

            // Assert
            Assert.Equal(nuevaSubcategoria, hilo.SubcategoriaId);
        }
    }
}