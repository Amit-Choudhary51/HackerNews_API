namespace HackerNews.Service.Models
{
    /// <summary>
    /// This class represents the the cache keys, that is used to store and retrive the data from cache.
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// Key to store new stories into cache.
        /// </summary>
        public static string NewStories => "_NewStories";
    }
}
