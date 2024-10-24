
using System.Linq.Expressions;

namespace TicketingSystem.Redis
{
    public interface IRedisCacheService
    {
        Task<bool> DeleteAsync(string key);
        Task<T?> GetAsync<T>(string key) where T : class;
        /// <summary>
        /// <para>Gets the value if it exists or saves if it doesn't</para>
        /// <para>Default expiry is 5 seconds</para>
        /// </summary>
        /// <returns></returns>
        Task<T> GetSaveAsync<T>(Func<Task<T>> value, string key, TimeSpan? expiry = null) where T : class;
        Task SaveAsync<T>(string key, T value, TimeSpan expiry) where T : class;
    }
}