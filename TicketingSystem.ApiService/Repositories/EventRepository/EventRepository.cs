using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;
namespace TicketingSystem.ApiService.Repositories.EventRepository
{
    public class EventRepository(TicketingDbContext context) : RepositoryBase<Event>(context), IEventRepository
    {
    }
}
