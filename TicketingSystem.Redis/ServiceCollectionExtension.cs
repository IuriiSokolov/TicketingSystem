using Microsoft.Extensions.DependencyInjection;

namespace TicketingSystem.Redis
{
    public static class ServiceCollectionExtension
    {
        public static void AddRedisCacheService(this IServiceCollection services)
        {
            services.AddScoped<IRedisCacheService, RedisCacheService>();
        }
    }
}
