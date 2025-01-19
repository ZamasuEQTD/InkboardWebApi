using Domain.Baneos;
using Domain.Categorias.Models;
using Domain.Comentarios.Models;
using Domain.Encuestas;
using Domain.Hilos.Models;
using Domain.Media.Models;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Persistence
{
    public class InkboardDbContext : IdentityDbContext<Usuario, IdentityRole<IdentityId>, IdentityId> {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias {get;set;}
        public DbSet<Encuesta> Encuestas {get; set;}
        public DbSet<Hilo> Hilos {get; set;}
        public DbSet<HashedMedia> Medias {get; set;}
        public DbSet<Subcategoria> Subcategorias {get; set;}
        public DbSet<Comentario> Comentarios {get; set;}
        public DbSet<Baneo> Baneos {get; set;}
        public InkboardDbContext(DbContextOptions<InkboardDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, isDevelopment) => {
                List<IdentityRole<IdentityId>> roles = [
                    new IdentityRole<IdentityId> {
                        Id = new IdentityId(Guid.NewGuid()),
                        Name = "Owner",
                        NormalizedName = "OWNER"
                    },
                    new IdentityRole<IdentityId> {
                        Id = new IdentityId(Guid.NewGuid()),
                        Name = "Moderador",
                        NormalizedName = "MODERADOR"
                    },
                    new IdentityRole<IdentityId> {
                        Id = new IdentityId(Guid.NewGuid()),
                        Name = "Anonimo",
                        NormalizedName = "ANONIMO"
                    }
                ];

                if(!context.Set<IdentityRole<IdentityId>>().Any()) {

                    context.Set<IdentityRole<IdentityId>>().AddRange(roles);

                    context.SaveChanges();
                }

                IdentityId ownerId = new IdentityId(Guid.NewGuid());

                if(!context.Set<Usuario>().Any()) {

                    var hasher = new PasswordHasher<Usuario>();

                    var usuario = new Usuario(){
                        Id = ownerId,
                        UserName = "Owner",
                        NormalizedUserName = "OWNER"
                    };
                    
                    usuario.PasswordHash = hasher.HashPassword(usuario, "PASSWORD1245");

                    context.Set<Usuario>().Add(usuario);
                
                    context.SaveChanges();
                }

                if(!context.Set<IdentityUserRole<IdentityId>>().Any()) {
                    context.Set<IdentityUserRole<IdentityId>>().AddRange([
                        new IdentityUserRole<IdentityId>() {
                            RoleId = roles[0].Id,
                            UserId =ownerId 
                        }
                    ]);

                    context.SaveChanges();

                }

                if(!context.Set<Categoria>().Any()){
                    List<Categoria> categorias = [ new Categoria("Tecnologia", false, [new Subcategoria("Inteligencia Artificial", "IA")])];
               
                    context.Set<Categoria>().AddRange(categorias);

                    context.SaveChanges();
                }
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().Property(b => b.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            
            builder.Entity<IdentityRole<IdentityId>>().Property(b => b.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.ApplyConfigurationsFromAssembly(typeof(InkboardDbContext).Assembly);
            
            base.OnModelCreating(builder);
        }
    }
}