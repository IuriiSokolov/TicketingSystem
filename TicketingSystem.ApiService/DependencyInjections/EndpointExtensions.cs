using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using TicketingSystem.ApiService.Endpoints;

namespace TicketingSystem.ApiService.DependencyInjections
{
    public static class EndpointExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] endpointServiceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false }
                    && type.IsAssignableTo(typeof(IEndpoints)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoints), type))
                .ToArray();

            services.TryAddEnumerable(endpointServiceDescriptors);
            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            var endpointGroups = app.Services.GetService<IEnumerable<IEndpoints>>()!;
            foreach (var endpoints in endpointGroups)
            {
                endpoints.MapEndpoints(app);
            }
            return app;
        }
    }
}
