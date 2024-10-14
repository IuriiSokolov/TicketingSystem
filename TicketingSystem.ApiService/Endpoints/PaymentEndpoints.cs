namespace TicketingSystem.ApiService.Endpoints
{
    public class PaymentEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var paymentGroup = app.MapGroup("api/paymnets");
        }
    }
}
