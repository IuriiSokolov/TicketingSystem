using TicketingSystem.ApiService.Services.PaymentService;

namespace TicketingSystem.ApiService.BackgroundWorkers
{
    public class SeatReleasingService : BackgroundService
    {
        private readonly IServiceScope _scope;
        private readonly TimeSpan CheckTime = TimeSpan.FromMinutes(1);
        public SeatReleasingService(IServiceProvider serviceProvider)
        {
            _scope = serviceProvider.CreateScope();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var paymentService = _scope.ServiceProvider.GetRequiredService<IPaymentService>();
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(CheckTime, stoppingToken);
                await paymentService.FailOutdatedPayments();
            }
        }
    }
}
