using Domain.Media.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{

    public class SpoileableMediaConfiguration : IEntityTypeConfiguration<MediaSpoileable>
    {
        public void Configure(EntityTypeBuilder<MediaSpoileable> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)) ;

            builder.HasOne<HashedMedia>(m=> m.HashedMedia).WithMany().HasForeignKey(r => r.HashedMediaId);
        }
    }

    public class HashedMediaConfiguration : IEntityTypeConfiguration<HashedMedia>
    {
        public void Configure(EntityTypeBuilder<HashedMedia> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("Id");
        
            builder.HasIndex(e => e.Hash).IsUnique();

            builder.OwnsOne(e => e.Media, m => {
                m.Property(e => e.Miniatura).HasColumnName("Miniatura");
                m.Property(e => e.Previsualizacion).HasColumnName("Previsualizacion")   ;
                m.OwnsOne(e => e.Provider, p => {
                    p.Property(e => e.Value).HasColumnName("Provider");
                });
            });
        }
    }
}