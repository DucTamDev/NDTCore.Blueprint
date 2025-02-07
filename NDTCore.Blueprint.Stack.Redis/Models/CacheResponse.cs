namespace NDTCore.Blueprint.Stack.Redis.Models
{
    public class CacheResponse<T> where T : class
    {
        public T Data { get; set; }

        public bool Found { get; set; }
    }

    public class HashCacheResponse<T> where T : class
    {
        public List<string> FoundFields { get; set; } = new List<string>();
        public List<string> NotFoundFields { get; set; } = new List<string>();
        public List<T> FoundValues { get; set; } = new List<T>();
    }
}
