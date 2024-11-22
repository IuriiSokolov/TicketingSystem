using TicketingSystem.ApiService.Repositories.EventRepository;
using TicketingSystem.ApiService.Repositories.PersonRepository;
using TicketingSystem.ApiService.Repositories.VenueRepository;
using TicketingSystem.ApiService.Repositories.SeatRepository;
using TicketingSystem.ApiService.Repositories.TickerRepository;
using TicketingSystem.ApiService.Repositories.SectionRepository;
using TicketingSystem.ApiService.Repositories.PriceCategoryRepository;
using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.ApiService.Repositories.CartRepository;
using TicketingSystem.ApiService.Services.EventService;
using TicketingSystem.ApiService.Services.VenueService;
using TicketingSystem.ApiService.Services.PaymentService;
using TicketingSystem.ApiService.Services.OrderService;
using TicketingSystem.ApiService.Repositories.UnitOfWork;
using TicketingSystem.ApiService.BackgroundWorkers;

namespace TicketingSystem.ApiService.DependencyInjections
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IPriceCategoryRepository, PriceCategoryRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IVenueService, VenueService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddHostedService<SeatReleasingService>();
        }
    }
}
