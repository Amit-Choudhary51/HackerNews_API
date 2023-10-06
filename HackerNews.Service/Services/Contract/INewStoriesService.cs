using HackerNews.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Service
{
    public interface INewStoriesService
    {
        List<int> GetNewStoryIDs();
        List<NewStoriesModel> GetNewStories();        
    }
}
