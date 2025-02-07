using NDTCore.Blueprint.Stack.Redis.Interfaces;
using NDTCore.Blueprint.Stack.Redis.Models;
using StackExchange.Redis;

namespace NDTCore.Blueprint.Stack.Redis.Services
{
    public class CacheHashService : CacheService, ICacheHashService
    {

        public CacheHashService()
        {

        }

        public Task<bool> DeleteHashFieldAsync(string hashKey, string hashField, bool isFireAndForget = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHashKeyAsync(string hashKey, bool isFireAndForget = false)
        {
            throw new NotImplementedException();
        }

        public Task<long> DeleteHashManyFieldsAsync(string hashKey, IList<string> fields, bool isFireAndForget = false)
        {
            throw new NotImplementedException();
        }

        public Task<CacheResponse<KeyValuePair<string, string>[]>> GetHashAllFieldsAsync(string hashKey, CommandFlags commandFlags = CommandFlags.PreferReplica)
        {
            throw new NotImplementedException();
        }

        public Task<CacheResponse<T>> GetHashAsync<T>(string hashKey, string hashField, CommandFlags commandFlags = CommandFlags.PreferReplica) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<CacheResponse<HashCacheResponse<T>>> GetHashMultipleFieldsAsync<T>(string hashKey, IList<string> hashFields, CommandFlags commandFlags = CommandFlags.None) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertHashAsync(string hashKey, string hashField, string hashValue, TimeSpan expiry, bool isFireAndForget = false)
        {
            throw new NotImplementedException();
        }

        public Task InsertHashManyFieldsAsync(string hashKey, KeyValuePair<string, string>[] values, TimeSpan expiry, bool isFireAndForget = false)
        {
            throw new NotImplementedException();
        }
    }
}
