using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.TickerRepository
{
    public class TicketRepository(TicketingDbContext context) : RepositoryBase<Ticket>(context), ITicketRepository
    {
    }
}
