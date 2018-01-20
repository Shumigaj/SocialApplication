using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SocialApplication.Business;
using SocialApplication.Controllers;
using SocialApplication.Core.Models;
using SocialApplication.Requests;
using SocialApplication.UnitTests.Content;
using SocialApplication.UnitTests.Core.Extensions;
using SocialApplication.UnitTests.Core.Variables;
using SocialApplication.Variables;

namespace SocialApplication.UnitTests.Controllers
{
    [TestFixture(Category = CategoryName.News)]
    public class NewsControllerTests : AutoMockedSubject<NewsController>
    {
        [Test]
        public async Task GetAsync_GetAllNews_ReturnAllNews()
        {
            // Arrange
            var expectedNewsCollection = new NewsCollection().Data;

            Use<INewsRepository>()
                .Setup(s => s.QueryAsync(It.IsAny<NewsSpecifications>()))
                .ReturnsAsync(expectedNewsCollection);

            // Act
            var actualResult = await Sub.GetAsync();

            // Assert
            CollectionAssert.AreEqual(expectedNewsCollection, actualResult);
        }

        [TestCase(-1)]
        [TestCase(100)]
        public async Task GetAsync_FindNonExistentNews_ReturnNotFound(int newsId)
        {
            // Arrange
            var expectedNewsQuery = new NewsCollection().Data
                                           .Where(w => w.Id == newsId)
                                           .ToList();

            Use<INewsRepository>()
                .Setup(s => s.QueryAsync(It.IsAny<NewsSpecifications>()))
                .ReturnsAsync(expectedNewsQuery);

            // Act
            var actualResult = await Sub.GetAsync(newsId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task GetAsync_FindNewsById_ReturnNews(int newsId)
        {
            // Arrange
            var expectedNewsQuery = new NewsCollection().Data
                .Where(w => w.Id == newsId)
                .ToList();

            var expectedNews = expectedNewsQuery.First();

            Use<INewsRepository>()
                .Setup(s => s.QueryAsync(It.IsAny<NewsSpecifications>()))
                .ReturnsAsync(expectedNewsQuery);

            // Act
            var actualResult = await Sub.GetAsync(newsId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(actualResult);
            var newsFromResponse = (News)((ObjectResult)actualResult).Value;
            Assert.AreEqual(expectedNews, newsFromResponse);
        }

        [Test]
        public void Create_NewsCanNotBeNull_ReturnBadRequest()
        {
            // Act
            var actualResult = Sub.Create(null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(actualResult);
        }
        
        [Test]
        public void Create_AddNews_ReturnNewsWithId()
        {
            // Arrange
            var newsId = 10;
            var news = new News
            {
                Id = newsId
            };
            Use<INewsRepository>()
                .Setup(s => s.Add(news))
                .Returns(news);

            // Act
            var actualResult = Sub.Create(news);

            // Assert
            Assert.IsInstanceOf<CreatedAtRouteResult>(actualResult);
            var createdAtRouteResult = (CreatedAtRouteResult)actualResult;
            var newsFromResult = (News)createdAtRouteResult.Value;
            Assert.AreEqual(news, newsFromResult);
            Assert.AreEqual(createdAtRouteResult.RouteName, RouteName.GetNews);
            CollectionAssert.AreEqual(new object[] { newsId }, createdAtRouteResult.RouteValues.Values);
        }
        
        [Test]
        public void Update_UseUninitialisedNewsValue_ReturnBadRequest()
        {
            // Act
            var actualResult = Sub.Update(0, null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(actualResult);
        }

        [Test]
        public void Update_NewsIdsDoesNotMatch_ReturnBadRequestObjectResult()
        {
            // Arrange
            var news = new News
            {
                Id = 11
            };

            // Act
            var actualResult = Sub.Update(10, news);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(actualResult);
        }

        [Test]
        public void Update_ChangeNonExistentNews_ReturnNotFoundResult()
        {
            // Arrange
            var news = new News
            {
                Id = 100
            };
            Use<INewsRepository>()
                .Setup(s => s.Update(news))
                .Returns((News)null);

            // Act
            var actualResult = Sub.Update(100, news);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void Update_ChangeNewsById_ReturnNoContentResult()
        {
            // Arrange
            var news = new News
            {
                Id = 0
            };
            Use<INewsRepository>()
                .Setup(s => s.Update(news))
                .Returns(news);

            // Act
            var actualResult = Sub.Update(news.Id, news);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(actualResult);
        }

        [Test]
        public void Update_UseNewsWithDuplicatedId_ReturnInvalidOperationException()
        {
            // Arrange
            var news = new News
            {
                Id = 0
            };
            Use<INewsRepository>()
                .Setup(s => s.Update(news))
                .Throws<InvalidOperationException>();

            // Act
            ActualValueDelegate<object> updateDelegate = () => Sub.Update(news.Id, news);

            // Assert
            Assert.That(updateDelegate, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Delete_RemoveNonExistentNews_ReturnNotFoundResult()
        {
            // Arrange
            Use<INewsRepository>()
                .Setup(s => s.Query(It.IsAny<NewsSpecifications>()))
                .Returns(Enumerable.Empty<News>());

            // Act
            var actualResult = Sub.Delete(100);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }
        
        [Test]
        public void Delete_RemoveExistentNews_ReturnNoContentResult()
        {
            // Arrange
            var news = new News
            {
                Id = 0
            };
            Use<INewsRepository>()
                .Setup(s => s.Query(It.IsAny<NewsSpecifications>()))
                .Returns(new List<News>{ news });
            
            // Act
            var actualResult = Sub.Delete(news.Id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(actualResult);
        }

        [Test]
        public void Delete_RemoveNewsWithDuplicatedId_ReturnInvalidOperationException()
        {
            // Arrange
            var news = new News
            {
                Id = 0
            };
            Use<INewsRepository>()
                .Setup(s => s.Query(It.IsAny<NewsSpecifications>()))
                .Returns(new List<News>{ news, news });

            // Act
            ActualValueDelegate<object> deleteDelegate = () => Sub.Delete(news.Id);

            // Assert
            Assert.That(deleteDelegate, Throws.TypeOf<InvalidOperationException>());
        }
    }
}
