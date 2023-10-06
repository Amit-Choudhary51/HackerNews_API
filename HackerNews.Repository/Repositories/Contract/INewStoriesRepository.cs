using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Repository
{
    public interface INewStoriesRepository
    {
        Task<HttpResponseMessage> GetNewStories();
        Task<HttpResponseMessage> GetNewStoryById(int Id);
    }
}
