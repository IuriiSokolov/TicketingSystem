using System.Data;

namespace TicketingSystem.ApiService.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task<(TOutput Result, string? ExceptionMsg)> DoInTransaction<TOutput>(Func<Task<TOutput>> task, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}