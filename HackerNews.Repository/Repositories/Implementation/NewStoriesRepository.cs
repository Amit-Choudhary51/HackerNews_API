using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Repository
{
    public class NewStoriesRepository : INewStoriesRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NewStoriesRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> GetNewStories()
        {
            var request = new HttpRequestMessage(
           HttpMethod.Get,
           "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty");
            request.Headers.Add("Accept", "application/json");           
            var client = _httpClientFactory.CreateClient();
            return await client.SendAsync(request);                    
        }

        public async Task<HttpResponseMessage> GetNewStoryById(int Id)
        {
            var request = new HttpRequestMessage(
           HttpMethod.Get,
           $"https://hacker-news.firebaseio.com/v0/item/{Id}.json?print=pretty");
            request.Headers.Add("Accept", "application/json");
            var client = _httpClientFactory.CreateClient();
            return await client.SendAsync(request);
        }
    }
}
