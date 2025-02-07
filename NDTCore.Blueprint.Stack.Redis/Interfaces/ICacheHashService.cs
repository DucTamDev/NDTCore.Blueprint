using NDTCore.Blueprint.Stack.Redis.Models;
using StackExchange.Redis;

namespace NDTCore.Blueprint.Stack.Redis.Interfaces
{
    public interface ICacheHashService
    {
        Task<bool> InsertHashAsync(string hashKey, string hashField, string hashValue, TimeSpan expiry, bool isFireAndForget = false);

        Task InsertHashManyFieldsAsync(string hashKey, KeyValuePair<string, string>[] values, TimeSpan expiry, bool isFireAndForget = false);

        Task<CacheResponse<T>> GetHashAsync<T>(string hashKey, string hashField, CommandFlags commandFlags = CommandFlags.PreferReplica) where T : class;

        Task<CacheResponse<HashCacheResponse<T>>> GetHashMultipleFieldsAsync<T>(string hashKey, IList<string> hashFields, CommandFlags commandFlags = CommandFlags.None) where T : class;

        Task<CacheResponse<KeyValuePair<string, string>[]>> GetHashAllFieldsAsync(string hashKey, CommandFlags commandFlags = CommandFlags.PreferReplica);

        Task<bool> DeleteHashFieldAsync(string hashKey, string hashField, bool isFireAndForget = false);

        Task<long> DeleteHashManyFieldsAsync(string hashKey, IList<string> fields, bool isFireAndForget = false);

        Task<bool> DeleteHashKeyAsync(string hashKey, bool isFireAndForget = false);
    }
}
