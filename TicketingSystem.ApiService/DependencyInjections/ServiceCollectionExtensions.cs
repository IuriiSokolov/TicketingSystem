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
using TicketingSystem.ApiService.Options;

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

            services.AddSingleton(TimeProvider.System);

            services.AddHostedService<SeatReleasingService>();
        }

        public static void AddOptions(this WebApplicationBuilder builder)
        {
            builder.AddOption<PaymentOptions>();
        }

        private static void AddOption<TOption>(this WebApplicationBuilder builder) where TOption : class
        {
            var typeName = typeof(TOption).Name;
            builder.Services.Configure<TOption>(builder.Configuration.GetSection(typeName));
        }
    }
}
