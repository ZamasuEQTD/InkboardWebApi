using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Hilos.Models;
using Domain.Usuarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    internal sealed class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(id => id.Value, value => new(value));


            builder.Property(c => c.Texto).HasConversion(text => text.Value, value => Texto.Create(value));

            builder.Property(c => c.Tag).HasConversion(tag => tag.Value, value => Tag.Create(value)).HasColumnName("tag");
            builder.Property(c => c.TagUnico).HasConversion(tagUnico => tagUnico.Value, value => TagUnico.Create(value)).HasColumnName("tag_unico");
            builder.Property(c => c.Dados).HasConversion(dados => dados.Value, value => Dados.Create(value)).HasColumnName("dados");


            builder.OwnsOne(c => c.Color, b =>
            {
                b.Property(c => c.Value).HasColumnName("color");
            });

            builder.OwnsMany(c => c.Respuestas, y =>
            {
                y.HasKey(c => c.Id);
                y.Property(c => c.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey(c => c.RespondidoId);

                y.HasOne<Comentario>().WithMany().HasForeignKey(c => c.RespuestaId);
            });

            builder.OwnsMany(c => c.Denuncias, y =>
            {
                y.HasKey(c => c.Id);
                y.Property(c => c.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey(d => d.ComentarioId);

                y.Property(h => h.DenuncianteId);
                y.HasOne<Usuario>().WithMany().HasForeignKey(h => h.DenuncianteId);

                y.Property(h => h.Status);
            });

            builder.OwnsMany(c => c.Interaciones, y =>
            {
                y.HasKey(c => c.Id);
                y.Property(c => c.Id).HasConversion(id => id.Value, value => new(value));

                y.HasOne<Usuario>().WithMany().HasForeignKey(r => r.UsuarioId);

                y.WithOwner().HasForeignKey(c => c.ComentarioId);
            });

            builder.HasOne<Usuario>().WithMany().HasForeignKey(c => c.AutorId);

            builder.HasOne<Hilo>().WithMany(h=> h.Comentarios).HasForeignKey(c => c.HiloId);
        }
    }

}