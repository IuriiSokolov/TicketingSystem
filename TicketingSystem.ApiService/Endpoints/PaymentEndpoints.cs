using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Services.PaymentService;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.ApiService.Endpoints
{
    public class PaymentEndpoints : IEndpoints
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var paymentGroup = app.MapGroup("api/payments");
            paymentGroup.MapGet("{paymentId}", GetPayment);
            paymentGroup.MapPost("{paymentId}/complete", CompletePayment);
            paymentGroup.MapPost("{paymentId}/failed", FailPayment);
        }

        private async Task<Results<Ok<PaymentStatus>, NotFound>> GetPayment(int paymentId, IPaymentService service)
        {
            var paymentStatus = await service.GetStatusByIdAsync(paymentId);
            return paymentStatus is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(paymentStatus.Value);
        }

        private async Task<Results<Ok, NotFound>> CompletePayment(int paymentId, IPaymentService service)
        {
            bool result = await service.CompletePayment(paymentId);
            return result
                ? TypedResults.Ok()
                : TypedResults.NotFound();
        }

        private async Task<Results<Ok, NotFound>> FailPayment(int paymentId, IPaymentService service)
        {
            bool result = await service.FailPayment(paymentId);
            return result
                ? TypedResults.Ok()
                : TypedResults.NotFound();
        }
    }
}
