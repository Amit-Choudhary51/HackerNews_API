using HackerNews.API.Controllers;
using HackerNews.Service;
using HackerNews.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class NewStoriesControllerTest
    {
        #region Variables
        private readonly Mock<INewStoriesService> _mockedNewStoriesService;
        private readonly Mock<IMemoryCache> _mockedMemoryCache;
        private readonly Mock<ICacheEntry> _mockedCacheEntry;
        private readonly NewStoriesController _newStoriesController;
        #endregion

        #region Constructor
        public NewStoriesControllerTest()
        {
            _mockedNewStoriesService = new Mock<INewStoriesService>();
            _mockedMemoryCache = new Mock<IMemoryCache>();
            _mockedCacheEntry = new Mock<ICacheEntry>();
            _newStoriesController = new NewStoriesController(_mockedNewStoriesService.Object, _mockedMemoryCache.Object);
        }
        #endregion

        #region Public Methods
        [Fact]
        public void GetNewstories_Test()
        {
            //Arrange
            List<NewStoriesModel> expectedNewstories = new List<NewStoriesModel>
            {
                new NewStoriesModel{ ID=1,Title="Impact of Mindfulness",Url="https://www.youtube.com/watch?v=pi9Xvh-Dva4" },
                new NewStoriesModel{ ID=2,Title="Ollama is now available",Url="https://ollama.ai/blog/ollama-is-now-available-as-an-official-docker-image"},
                new NewStoriesModel{ ID=3,Title="The Ultimate Guide to Open Source",Url="https://getstream.io/blog/open-source-guide/"},
                new NewStoriesModel{ ID=4,Title="Communicative Agents for Software Development",Url="https://arxiv.org/abs/2307.07924"},
            };
            
            _mockedMemoryCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                              .Returns(_mockedCacheEntry.Object);

            _mockedNewStoriesService.Setup(x => x.GetNewStories())
                                    .Returns(expectedNewstories);

            // Act
            var response = _newStoriesController.Get();
          
            //Assert
            Assert.NotNull(response);
            Assert.Equal(expectedNewstories, response);

        }

        [Fact]
        public void GetNewstories_Null_Response_Test()
        {
            //Arrange
            List<NewStoriesModel> expectedNewstories = null;
            
            _mockedMemoryCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                              .Returns(_mockedCacheEntry.Object);

            _mockedNewStoriesService.Setup(x => x.GetNewStories())
                                    .Returns(expectedNewstories);

            // Act
            var response = _newStoriesController.Get();

            //Assert
            Assert.Null(response);
            Assert.Equal(expectedNewstories, response);

        }

        [Fact]
        public void GetNewstories_From_Cache_IF_Data_Available_In_Cache_Test()
        {
            //Arrange
            List<NewStoriesModel> serviceData = new List<NewStoriesModel>
            {
                new NewStoriesModel{ ID=1,Title="Impact of Mindfulness",Url="https://www.youtube.com/watch?v=pi9Xvh-Dva4" },
                new NewStoriesModel{ ID=2,Title="Ollama is now available",Url="https://ollama.ai/blog/ollama-is-now-available-as-an-official-docker-image"},
                new NewStoriesModel{ ID=3,Title="The Ultimate Guide to Open Source",Url="https://getstream.io/blog/open-source-guide/"},
                new NewStoriesModel{ ID=4,Title="Communicative Agents for Software Development",Url="https://arxiv.org/abs/2307.07924"},
            };

            object cachedData = new List<NewStoriesModel>
            {
                new NewStoriesModel{ ID=5,Title="US transition to clean energy",Url="https://www.npr.org/2023/10/05/1203846437/u-s-transition-to-clean-energy-is-happening-faster-than-you-think-reporter-says" },
                new NewStoriesModel{ ID=6,Title="Alternative to Democracy",Url="https://en.wikipedia.org/wiki/Liquid_democracy"},
                new NewStoriesModel{ ID=7,Title="Bevy: Game Engine of the Future [video]",Url="https://www.youtube.com/watch?v=sfFQrhajs6o"}
            };

            _mockedMemoryCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                             .Returns(_mockedCacheEntry.Object);

            _mockedMemoryCache.Setup(m => m.TryGetValue(It.IsAny<string>(), out cachedData))
                             .Returns(true);

            _mockedNewStoriesService.Setup(x => x.GetNewStories())
                                    .Returns(serviceData);

            // Act
            var response = _newStoriesController.Get();

           // Assert            
            Assert.NotNull(response);
            Assert.Equal(cachedData, response);
            Assert.NotEqual(serviceData, response);

        }

        [Fact]
        public void Insert_Data_In_Cache_Test()
        {
            //Arrange
            List<NewStoriesModel> stories = new List<NewStoriesModel>
            {
                new NewStoriesModel{ ID=1,Title="Impact of Mindfulness",Url="https://www.youtube.com/watch?v=pi9Xvh-Dva4" },
                new NewStoriesModel{ ID=2,Title="Ollama is now available",Url="https://ollama.ai/blog/ollama-is-now-available-as-an-official-docker-image"},
                new NewStoriesModel{ ID=3,Title="The Ultimate Guide to Open Source",Url="https://getstream.io/blog/open-source-guide/"},
                new NewStoriesModel{ ID=4,Title="Communicative Agents for Software Development",Url="https://arxiv.org/abs/2307.07924"},
            };

            _mockedMemoryCache.Object.Remove("somekey");

            _mockedMemoryCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                             .Returns(_mockedCacheEntry.Object);

            //Act
            var cacheResponse = _mockedMemoryCache.Object.Set("somekey", stories);

            //Assert
            Assert.NotNull(cacheResponse);
            Assert.Equal(stories, cacheResponse);
        }

        [Fact]
        public void Insert_And_Retrive_Data_From_Cache_Test()
        {
            // Arrange
            object stories = new List<NewStoriesModel>
            {
                new NewStoriesModel{ ID=1,Title="Impact of Mindfulness",Url="https://www.youtube.com/watch?v=pi9Xvh-Dva4" },
                new NewStoriesModel{ ID=2,Title="Ollama is now available",Url="https://ollama.ai/blog/ollama-is-now-available-as-an-official-docker-image"},
                new NewStoriesModel{ ID=3,Title="The Ultimate Guide to Open Source",Url="https://getstream.io/blog/open-source-guide/"},
                new NewStoriesModel{ ID=4,Title="Communicative Agents for Software Development",Url="https://arxiv.org/abs/2307.07924"},
            };

            List<NewStoriesModel> expectedData = new List<NewStoriesModel>();

            _mockedMemoryCache.Object.Remove("somekey");

            _mockedMemoryCache.Setup(m => m.CreateEntry(It.IsAny<string>()))
                             .Returns(_mockedCacheEntry.Object);

            _mockedMemoryCache.Setup(m => m.TryGetValue(It.IsAny<string>(), out stories))
                             .Returns(true);

            _mockedMemoryCache.Object.Set("somekey", stories);

            // Act
            var response = _mockedMemoryCache.Object.TryGetValue("somekey", out expectedData);

            // Assert
            Assert.True(response);
            Assert.Equal(stories, expectedData);
        }
        #endregion
    }
}
