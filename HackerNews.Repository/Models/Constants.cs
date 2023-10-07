namespace HackerNews.Repository.Models
{
    /// <summary>
    /// This class represents the constant variables.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Base url for hacker news api.
        /// </summary>
        public const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";
        /// <summary>
        /// Api to get collection of new stories.
        /// </summary>
        public const string NewstoriesApi = "newstories.json?print=pretty";
        /// <summary>
        /// Api to get story by Id.
        /// </summary>
        public const string StoryByIdApi = "item/{0}.json?print=pretty";
    }
}
