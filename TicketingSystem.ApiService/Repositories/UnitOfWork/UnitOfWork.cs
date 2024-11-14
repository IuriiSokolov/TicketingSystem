using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using TicketingSystem.Common.Context;

namespace TicketingSystem.ApiService.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketingDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(TicketingDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var transaction = _dbContext.Database.BeginTransaction(isolationLevel);
            return transaction.GetDbTransaction();
        }

        public async Task<(TOutput Result, string? ExceptionMsg)> DoInTransaction<TOutput>(Func<Task<TOutput>> task, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var strat = _dbContext.Database.CreateExecutionStrategy();
            var result = await strat.ExecuteAsync(() => ExecuteInTransaction(task, isolationLevel));
            return result;
        }

        private async Task<(TOutput Result, string? ExceptionMsg)> ExecuteInTransaction<TOutput>(Func<Task<TOutput>> task, IsolationLevel isolationLevel)
        {
            using var transaction = BeginTransaction(isolationLevel);
            try
            {
                var result = await task();
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
                return (result, null);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                var errorMsg = $"Error during the transaction execution. {ex.Message}";
                _logger.LogError(errorMsg);
                return (default!, errorMsg);
            }
        }

        public async Task SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();
    }
}
