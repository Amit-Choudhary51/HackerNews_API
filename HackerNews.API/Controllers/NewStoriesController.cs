using HackerNews.Service;
using HackerNews.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace HackerNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewStoriesController : ControllerBase
    {
        #region Variables
        private readonly INewStoriesService _newStoriesService;
        private readonly IMemoryCache _cache;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="newStoriesService"></param>
        /// <param name="cache"></param>
        public NewStoriesController(INewStoriesService newStoriesService, IMemoryCache cache)
        {
            _newStoriesService = newStoriesService;
            _cache = cache;
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Get collection of new stories api.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<NewStoriesModel> Get()
        {

            if (!_cache.TryGetValue(CacheKeys.NewStories, out List<NewStoriesModel> newStories))
            {
                newStories = _newStoriesService.GetNewStories();

                var cacheEntryOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5),
                    Size = 1024
                };

                _cache.Set(CacheKeys.NewStories, newStories, cacheEntryOption);

            }
            return newStories;
        }
        #endregion
    }
}
