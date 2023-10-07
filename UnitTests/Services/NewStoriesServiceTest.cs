using HackerNews.Repository;
using HackerNews.Service;
using HackerNews.Service.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class NewStoriesServiceTest
    {
        #region Variables
        private readonly Mock<INewStoriesRepository> _mockedNewStoriesRepository;
        private readonly NewStoriesService _newStoriesService;
        #endregion

        #region Constructor
        public NewStoriesServiceTest()
        {
            _mockedNewStoriesRepository = new Mock<INewStoriesRepository>();
            _newStoriesService = new NewStoriesService(_mockedNewStoriesRepository.Object);
        }
        #endregion

        #region Public Methods
        [Fact]
        public void Get_NewStories_Ids_Test()
        {
            // Arrange
            List<int> Ids = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var httpResponseMessage = CreateHttpResponseMessage(Ids);

            _mockedNewStoriesRepository.Setup(x => x.GetNewStories())
                .Returns(Task.FromResult(httpResponseMessage));

            // Act
            var result = _newStoriesService.GetNewStoryIDs();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Ids, result);
        }

        [Fact]
        public void Get_Null_NewStories_Ids_Test()
        {
            // Arrange
            List<int> Ids = null;
            var httpResponseMessage = CreateHttpResponseMessage(Ids);

            _mockedNewStoriesRepository.Setup(x => x.GetNewStories())
                .Returns(Task.FromResult(httpResponseMessage));

            // Act
            var result = _newStoriesService.GetNewStoryIDs();

            // Assert
            Assert.Null(result);
            Assert.Equal(Ids, result);
        }

        [Fact]
        public void Get_Empty_NewStories_Ids_Test()
        {
            // Arrange
            List<int> Ids = new List<int>();
            var httpResponseMessage = CreateHttpResponseMessage(Ids);

            _mockedNewStoriesRepository.Setup(x => x.GetNewStories())
                .Returns(Task.FromResult(httpResponseMessage));

            // Act
            var result = _newStoriesService.GetNewStoryIDs();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.Equal(Ids, result);
        }

        [Fact]
        public void Get_NewStories_Test()
        {
            // Arrange
            List<int> Ids = new List<int> { 1, 2, 3, 4 };
            var httpResponseMessageForIds = CreateHttpResponseMessage(Ids);

            _mockedNewStoriesRepository.Setup(x => x.GetNewStories())
                                       .Returns(Task.FromResult(httpResponseMessageForIds));

            List<NewStoriesModel> expectedNewstories = new List<NewStoriesModel>();

            Ids.ForEach(Id =>
            {
                var data = new NewStoriesModel
                {
                    ID = Id,
                    Title = $"Title{Id}",
                    Url = $"Url{Id}"
                };

                expectedNewstories.Add(data);

                var httpResponseMessageForModel = CreateHttpResponseMessage(data);
                _mockedNewStoriesRepository.Setup(x => x.GetNewStoryById(Id))
                                           .Returns(Task.FromResult(httpResponseMessageForModel));

            });

            // Act
            var result = _newStoriesService.GetNewStories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedNewstories.Count, result.Count);
        }

        [Fact]
        public void Throw_Exception_If_List_Of_Ids_Null_Test()
        {
            // Arrange
            List<int> Ids = null;
            var httpResponseMessageForIds = CreateHttpResponseMessage(Ids);

            _mockedNewStoriesRepository.Setup(x => x.GetNewStories())
                                       .Returns(Task.FromResult(httpResponseMessageForIds));

            // Act
            var caughtException = Assert.Throws<ArgumentNullException>(() => _newStoriesService.GetNewStories());

            // Assert
            Assert.Contains("Value cannot be null.", caughtException.Message);
        }

        [Fact]
        public void Get_Empty_Stort_List_When_Story_Ids_List_Empty_Test()
        {
            // Arrange
            List<int> Ids = new List<int>();
            var httpResponseMessageForIds = CreateHttpResponseMessage(Ids);

            _mockedNewStoriesRepository.Setup(x => x.GetNewStories())
                                       .Returns(Task.FromResult(httpResponseMessageForIds));

            // Act
            var result = _newStoriesService.GetNewStories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Create HttpResponseMessage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private HttpResponseMessage CreateHttpResponseMessage<T>(T data)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            var jsonSerializedObject = JsonConvert.SerializeObject(data);
            var byteArray = System.Text.Encoding.UTF8.GetBytes(jsonSerializedObject);
            var byteContent = new ByteArrayContent(byteArray);

            responseMessage.Content = byteContent;

            return responseMessage;
        }
        #endregion

    }
}
