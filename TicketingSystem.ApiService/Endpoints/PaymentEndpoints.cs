namespace TicketingSystem.ApiService.Endpoints
{
    public static class PaymentEndpoints
    {
        public static void Add(IEndpointRouteBuilder app)
        {
            using (var scope = app.ServiceProvider.CreateScope())
            {
                //var userGroup = app.MapGroup("api/events");
                //userGroup.MapGet("", GetEvents);
                //userGroup.MapGet("id", GetEvent);
                //userGroup.MapPost("", AddEvent);
                //userGroup.MapPut("", UpdateEvent);
                //userGroup.MapDelete("", DeleteEvent);
            }
        }
    }
}
