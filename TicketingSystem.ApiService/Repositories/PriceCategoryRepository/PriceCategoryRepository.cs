using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.ApiService.Repositories.RepositoryBase;
using TicketingSystem.Common.Context;
using TicketingSystem.Common.Model.Database.Entities;

namespace TicketingSystem.ApiService.Repositories.PriceCategoryRepository
{
    public class PriceCategoryRepository(TicketingDbContext context) : RepositoryBase<PriceCategory>(context), IPriceCategoryRepository
    {
    }
}
