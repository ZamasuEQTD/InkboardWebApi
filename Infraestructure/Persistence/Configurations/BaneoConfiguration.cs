
using Domain.Baneos;
using Domain.Usuarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class BaneoConfiguration : IEntityTypeConfiguration<Baneo>
    {
        public void Configure(EntityTypeBuilder<Baneo> builder)
        {

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasConversion(id => id.Value, value => new(value));
            
            builder.HasOne<Usuario>().WithMany().HasForeignKey(b => b.UsuarioBaneadoId);

            builder.HasOne<Usuario>().WithMany().HasForeignKey(b => b.ModeradorId);
        }
    }
}