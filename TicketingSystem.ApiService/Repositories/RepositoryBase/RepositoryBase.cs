using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Common.Context;

namespace TicketingSystem.ApiService.Repositories.RepositoryBase
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly TicketingDbContext Context;

        public RepositoryBase(TicketingDbContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            Context.Set<TEntity>().Remove(entity);
            return true;
        }

        public async Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> queriable = Context.Set<TEntity>();
            for (int i = 0; i < includes.Length; i++)
            {
                queriable = queriable.Include(includes[i]);
            }
            return await queriable.Where(predicate).ToListAsync();
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> queriable = Context.Set<TEntity>();
            for (int i = 0; i < includes.Length; i++)
            {
                queriable = queriable.Include(includes[i]);
            }
            return await queriable.FirstOrDefaultAsync(predicate);
        }
    }
}
