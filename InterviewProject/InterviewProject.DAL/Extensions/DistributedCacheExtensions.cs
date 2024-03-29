using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace InterviewProject.DAL.Extensions;

public static class DistributedCacheExtensions
{
    public static Task SetEntityAsync<TEntity>(
        this IDistributedCache cache,
        string key,
        TEntity entity,
        DistributedCacheEntryOptions entryOptions,
        CancellationToken cancellationToken = default) where TEntity : class
        => cache.SetAsync(
            key,
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity)),
            entryOptions,
            cancellationToken);

    public static async Task<TEntity?> GetEntityAsync<TEntity>(
        this IDistributedCache cache,
        string key,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var value = await cache.GetAsync(key, cancellationToken);

        if (value is null)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<TEntity>(Encoding.UTF8.GetString(value));
    }
}