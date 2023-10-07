using HackerNews.Repository.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Repository
{
    /// <summary>
    /// This class represent to call the hacker news api.
    /// </summary>
    public class NewStoriesRepository : INewStoriesRepository
    {
        #region Variables
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public NewStoriesRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Get new stories from hacker news api.
        /// </summary>
        /// <returns></returns>
        public Task<HttpResponseMessage> GetNewStories() => Get($"{Constants.BaseUrl}{Constants.NewstoriesApi}");       

        /// <summary>
        /// Get new story by Id from hacker new api.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public  Task<HttpResponseMessage> GetNewStoryById(int Id)=> Get(string.Format($"{Constants.BaseUrl}{Constants.StoryByIdApi}", Id));
        
        #endregion

        #region Private Methods
        /// <summary>
        /// Get api call.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,url);
            request.Headers.Add("Accept", "application/json");
            var client = _httpClientFactory.CreateClient();

            return await client.SendAsync(request);
        }
        #endregion
    }
}
