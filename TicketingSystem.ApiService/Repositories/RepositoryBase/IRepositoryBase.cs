
namespace TicketingSystem.ApiService.Repositories.RepositoryBase
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        TEntity Update(TEntity entity);
    }
}