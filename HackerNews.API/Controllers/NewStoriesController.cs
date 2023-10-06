using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Service;
using HackerNews.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class NewStoriesController : ControllerBase
    {
        private readonly INewStoriesService _newStoriesService;
        private readonly IMemoryCache _cache;
        public NewStoriesController(INewStoriesService newStoriesService, IMemoryCache cache)
        {
            _newStoriesService = newStoriesService;
            _cache = cache;
        }        

        [HttpGet]
        public IEnumerable<NewStoriesModel> Get()
        {
            if(!_cache.TryGetValue(CacheKeys.NewStories,out List<NewStoriesModel> newStories))
            {
                newStories=_newStoriesService.GetNewStories();

                var cacheEntryOption = new MemoryCacheEntryOptions { 
                AbsoluteExpiration=DateTime.Now.AddMinutes(10),
                SlidingExpiration=TimeSpan.FromMinutes(5),
                Size=1024
                };

                _cache.Set(CacheKeys.NewStories, newStories, cacheEntryOption);

            }
            return newStories;
        }
    }
}
