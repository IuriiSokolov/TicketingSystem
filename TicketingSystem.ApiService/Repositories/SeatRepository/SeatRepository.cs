﻿using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.SeatRepository
{
    public class SeatRepository(TicketingDbContext context) : RepositoryBase<Seat>(context), ISeatRepository
    {
    }
}
