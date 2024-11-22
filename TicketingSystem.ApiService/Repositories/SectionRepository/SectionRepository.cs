using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.SectionRepository
{
    public class SectionRepository(TicketingDbContext context) : RepositoryBase<Section>(context), ISectionRepository
    {
    }
}
