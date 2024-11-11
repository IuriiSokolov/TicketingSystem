namespace TicketingSystem.Common.Model.DTOs.Other
{
    public record Email
    {
        public string Address { get; init; } = "";
        public string Subject { get; init; } = "";
        public string Message { get; init; } = "";

        public Email(string address, string subject, string message)
        {
            Address = address;
            Subject = subject;
            Message = message;
        }
    }
}
