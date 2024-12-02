namespace TicketingSystem.ApiService.Options
{
    public class PaymentOptions
    {
        public const string Key = "PaymentShelfLifeMin";
        public int PaymentShelfLifeMin { get; set; }
        public TimeSpan PaymentShelfLifeMinTime => TimeSpan.FromMinutes(PaymentShelfLifeMin);
    }
}
