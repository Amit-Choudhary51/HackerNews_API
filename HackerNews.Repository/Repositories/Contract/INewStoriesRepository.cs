using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Repository
{
    public interface INewStoriesRepository
    {
        /// <summary>
        /// Get new stories from hacker news api.
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetNewStories();

        /// <summary>
        /// Get new story by Id from hacker new api.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetNewStoryById(int Id);
    }
}
