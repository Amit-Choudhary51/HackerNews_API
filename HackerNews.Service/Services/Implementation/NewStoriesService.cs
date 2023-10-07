using HackerNews.Repository;
using HackerNews.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Service
{
    /// <summary>
    /// This class represents to fetch new stories.
    /// </summary>
    public class NewStoriesService : INewStoriesService
    {
        #region Variables
        private readonly INewStoriesRepository _newStoriesRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="newStoriesRepository"></param>
        public NewStoriesService(INewStoriesRepository newStoriesRepository)
        {
            _newStoriesRepository = newStoriesRepository;
        }
        #endregion

        #region Public Methods  

        /// <summary>
        /// Get list of New Story Id's.
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
        /// Get list of new stories.
        /// </summary>
        /// <returns></returns>
        public List<NewStoriesModel> GetNewStories()
        {
            List<int> newStoriesIds = GetNewStoryIDs().Take(200).ToList();

            List<NewStoriesModel> newStories = new List<NewStoriesModel>();           

            newStoriesIds.ForEach(storyId =>
            {
                HttpResponseMessage response = _newStoriesRepository.GetNewStoryById(storyId).Result;
                var finalData = response.Content.ReadAsStringAsync().Result;
                var dataResponse = JsonConvert.DeserializeObject<NewStoriesModel>(finalData);

                newStories.Add(dataResponse);
            });

            return newStories;
        }
    }
    #endregion

}
