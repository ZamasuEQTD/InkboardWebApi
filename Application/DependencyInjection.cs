using Microsoft.Extensions.DependencyInjection;
using MediatR;
namespace Application
{
    public static class DependencyInjection
    {
        static public IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.Configuration.AssemblyReference.Assembly))
            .Scan(x => x.FromAssemblies(Configuration.AssemblyReference.Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces().WithScopedLifetime());
            return services;
        }
    }
}