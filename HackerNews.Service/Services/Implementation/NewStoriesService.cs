using HackerNews.Repository;
using HackerNews.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Service
{
    public class NewStoriesService : INewStoriesService
    {
        private readonly INewStoriesRepository _newStoriesRepository;
        
        public NewStoriesService(INewStoriesRepository newStoriesRepository)
        {
            _newStoriesRepository = newStoriesRepository;
        }

        /// <summary>
        /// Get New Story Id's
        /// </summary>
        /// <returns></returns>
        public List<int> GetNewStoryIDs()
        {
            HttpResponseMessage response = _newStoriesRepository.GetNewStories().Result;
            var finalData = response.Content.ReadAsStringAsync().Result;
            var dataResponse = JsonConvert.DeserializeObject<List<int>>(finalData);
            return dataResponse;

        }

        /// <summary>
        /// Get New stories by ID
        /// </summary>
        /// <returns></returns>
        public List<NewStoriesModel> GetNewStories()
        {           
            List<int> newStoriesIds = GetNewStoryIDs().ToList();           

            List<NewStoriesModel> newStories = new List<NewStoriesModel>();

            Parallel.ForEach(newStoriesIds, storyId =>
            {
                HttpResponseMessage response = _newStoriesRepository.GetNewStoryById(storyId).Result;
                var finalData = response.Content.ReadAsStringAsync().Result;
                var dataResponse = JsonConvert.DeserializeObject<NewStoriesModel>(finalData);

                newStories.Add(dataResponse);
            });
            
            return newStories;
        }
    }
}
