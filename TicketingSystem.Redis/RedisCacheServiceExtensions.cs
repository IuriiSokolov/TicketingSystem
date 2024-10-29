namespace TicketingSystem.Redis
{
    public static class RedisCacheServiceExtensions
    {
        public static async Task<T> GetSaveAsync<T>(this IRedisCacheService cache, Func<Task<T>> value, string key, TimeSpan? expiry = null) where T : class
        {
            expiry ??= TimeSpan.FromSeconds(5);
            var result = await cache.GetAsync<T>(key);
            if (result is null)
            {
                result = await value();
                await cache.SaveAsync(key, result, expiry.Value);
            }
            return result;
        }
    }
}
