﻿using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Common.Model.Database.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public CartStatus CartStatus { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }

        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public ICollection<Ticket> Tickets { get; } = [];
    }
}
