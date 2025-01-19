using Domain.Encuestas;
using Domain.Usuarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
        internal class EncuestaConfiguration : IEntityTypeConfiguration<Encuesta>
    {
        public void Configure(EntityTypeBuilder<Encuesta> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value));

            builder.OwnsMany(e => e.Respuestas, y =>
            {

                y.ToTable("respuestas");
                y.HasKey(r => r.Id);
                y.Property(r => r.Id).HasConversion(id => id.Value, value => new(value));

                y.WithOwner().HasForeignKey("encuesta_id");
            });

            builder.OwnsMany(e => e.Votos, y =>
            {

                y.ToTable("votos");
                y.HasKey(r => r.Id);
                y.Property(r => r.Id).HasConversion(id => id.Value, value => new(value));

                y.HasOne<Usuario>().WithMany().HasForeignKey(v => v.VotanteId);

                y.Property(r => r.RespuestaId).HasConversion(id => id.Value, value => new(value));
            });
        }

    }
}