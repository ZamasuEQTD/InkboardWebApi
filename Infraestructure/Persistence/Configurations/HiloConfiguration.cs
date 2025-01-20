using Domain.Categorias.Models;
using Domain.Comentarios.Models;
using Domain.Encuestas;
using Domain.Hilos.Models;
using Domain.Media.Models;
using Domain.Notificaciones;
using Domain.Usuarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    internal sealed class HiloConfiguration : IEntityTypeConfiguration<Hilo>
    {
        public void Configure(EntityTypeBuilder<Hilo> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
        
            builder.OwnsOne(h => h.Configuracion, conf =>
                {
                    conf.Property(c => c.DadosActivado).HasColumnName("dados_activado");
                    conf.Property(c => c.IdUnicoActivado).HasColumnName("id_unico_activado");
                }
            );

            builder.OwnsOne(h => h.Sticky, y =>
            {
                y.ToTable("stickies");
                
                y.HasKey(s => s.Id);
                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey(s => s.HiloId);
            });

            builder.OwnsMany(h => h.Denuncias, y =>
            {

                y.HasKey(h => h.Id);
                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey(d => d.HiloId);

                y.HasOne<Usuario>().WithMany().HasForeignKey(h => h.DenuncianteId);
            });

            builder.OwnsMany(h => h.Interacciones, y =>
            {
                y.ToTable("hilo_interacciones");

                y.HasKey(r => r.Id);

                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey(r => r.HiloId);

                y.HasOne<Usuario>().WithMany().HasForeignKey(r => r.UsuarioId);
            });

            builder.OwnsMany(h => h.ComentariosDestacados, y =>
            {
                y.HasKey(c => c.Id);
                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey(c => c.HiloId);

                y.HasOne<Comentario>().WithMany().HasForeignKey(c => c.ComentarioId);
            });

            builder.HasOne<Usuario>().WithMany().HasForeignKey(h=> h.AutorId);
            builder.HasOne<Subcategoria>().WithMany().HasForeignKey(h=> h.SubcategoriaId);
            builder.HasOne<Encuesta>().WithOne().HasForeignKey<Hilo>(h => h.EncuestaId);
            builder.HasOne<MediaSpoileable>().WithOne().HasForeignKey<Hilo>(h => h.PortadaId);
        }    
     }    
}