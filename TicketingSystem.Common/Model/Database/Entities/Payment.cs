﻿namespace TicketingSystem.Common.Model.Database.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public required DateTime PaymentTime { get; set; }

        public Cart? Cart { get; set; }
    }
}
