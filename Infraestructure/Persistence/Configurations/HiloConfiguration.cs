using Domain.Hilos.Models;
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
                    conf.Property(c => c.Dados).HasColumnName("Dados");
                    conf.Property(c => c.IdUnicoActivado).HasColumnName("IdUnicoActivado");
                }
            );

            builder.OwnsOne(h => h.Sticky, y =>
            {
                y.ToTable("Sticky");
                
                y.HasKey(s => s.Id);
                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value));

                y.Property(c => c.Hilo);

                y.WithOwner().HasForeignKey(s => s.Hilo);
            });

            builder.HasOne<Usuario>().WithMany().HasForeignKey(h=> h.AutorId);
            
        }    
     }    
}