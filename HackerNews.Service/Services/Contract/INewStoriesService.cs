using HackerNews.Service.Models;
using System.Collections.Generic;

namespace HackerNews.Service
{
    public interface INewStoriesService
    {
        /// <summary>
        /// Get list of New Story Id's.
        /// </summary>
        /// <returns></returns>
        List<int> GetNewStoryIDs();
        /// <summary>
        /// Get list of new stories.
        /// </summary>
        /// <returns></returns>
        List<NewStoriesModel> GetNewStories();       
    }
}
