using TicketingSystem.ApiService.Repositories.EventRepository;
using TicketingSystem.ApiService.Repositories.PersonRepository;
using TicketingSystem.ApiService.Repositories.VenueRepository;
using TicketingSystem.ApiService.Repositories.SeatRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;

namespace TicketingSystem.ApiService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
        }
    }
}
