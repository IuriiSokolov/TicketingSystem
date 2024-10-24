using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PersonRepository
{
    public class PersonRepository(TicketingDbContext context) : RepositoryBase<Person>(context), IPersonRepository
    {
    }
}
