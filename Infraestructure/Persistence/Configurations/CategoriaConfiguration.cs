using Domain.Categorias.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    internal sealed class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
        }    
     }    

    internal sealed class SubcategoriaConfiguration : IEntityTypeConfiguration<Subcategoria>
    {   
      public void Configure(EntityTypeBuilder<Subcategoria> builder)
      {
          builder.HasKey(h => h.Id);
          builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
      }    
    }    
}