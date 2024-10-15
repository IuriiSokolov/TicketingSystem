
using Microsoft.AspNetCore.Http.HttpResults;
using TicketingSystem.ApiService.Repositories.PaymentRepository;
using TicketingSystem.Common.Model.Database.Entities;
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
            paymentGroup.MapPost("{payment_id}/failed", FailPayment);
        }

        private async Task<Results<Ok<PaymentStatus>, NotFound>> GetPayment(int paymentId, IPaymentRepository repo)
        {
            var payment = await repo.GetByIdAsync(paymentId);
            return payment != null
                ? TypedResults.Ok(payment.PaymentStatus)
                : TypedResults.NotFound();
        }

        private async Task<Results<Ok, NotFound>> CompletePayment(int paymentId, IPaymentRepository repo)
        {
            bool result = await repo.CompletePayment(paymentId);
            return result
                ? TypedResults.Ok()
                : TypedResults.NotFound();
        }

        private async Task<Results<Ok, NotFound>> FailPayment(int paymentId, IPaymentRepository repo)
        {
            bool result = await repo.FailPayment(paymentId);
            return result
                ? TypedResults.Ok()
                : TypedResults.NotFound();
        }
    }
}
