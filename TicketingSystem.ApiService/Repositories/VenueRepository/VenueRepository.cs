using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.VenueRepository
{
    public class VenueRepository(TicketingDbContext context) : RepositoryBase<Venue>(context), IVenueRepository
    {
    }
}
