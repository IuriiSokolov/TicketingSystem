
namespace TicketingSystem.ApiService.Repositories.RepositoryBase
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}