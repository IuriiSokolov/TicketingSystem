﻿
using System.Linq.Expressions;

namespace TicketingSystem.Redis
{
    public interface IRedisCacheService
    {
        Task<bool> DeleteAsync(string key);
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SaveAsync<T>(string key, T value, TimeSpan expiry) where T : class;
    }
}