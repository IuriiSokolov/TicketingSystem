using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.Tests.RepositoryTests.RepositoryBaseTests
{
    public class TestRepository(TicketingDbContext context) : RepositoryBase<Venue>(context)
    {
    }
}
