﻿namespace TicketingSystem.Common.Model
{
    public class Seat
    {
        public required int Id { get; set; }
        public required Event Event { get; set; }
        public required SeatStatus Status { get; set; }
    }
}
