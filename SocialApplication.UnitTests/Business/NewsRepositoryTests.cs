using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SocialApplication.Business;
using SocialApplication.Core.Contracts;
using SocialApplication.Core.Models;
using SocialApplication.Requests;
using SocialApplication.UnitTests.Content;
using SocialApplication.UnitTests.Core.Extensions;
using SocialApplication.UnitTests.Core.Variables;

namespace SocialApplication.UnitTests.Business
{

    [TestFixture(Category = CategoryName.News)]
    public class NewsRepositoryTests : AutoMockedSubject<NewsRepository>
    {
        [Test]
        public void Query_SpecificationIsNull_ReturnAllNews()
        {
            // Arrange
            var newsCollection = new NewsCollection().Data;
            Use<INewsProvider>()
                .Setup(c => c.GetAll())
                .Returns(newsCollection);

            // Act
            var actualResult = Sub.Query().ToList();

            // Assert
            CollectionAssert.AreEqual(newsCollection,actualResult);
        }
        
        [TestCase(null, 4)]
        [TestCase(0, 1)]
        [TestCase(100, 0)]
        public void Query_ApplyFilter_RenurnFilteredResult(int? newsId, int newsCount)
        {
            // Arrange
            var newsCollection = new NewsCollection().Data;
            Use<INewsProvider>()
                .Setup(c => c.GetAll())
                .Returns(newsCollection);

            var expectedResult = newsId.HasValue
                ? newsCollection.Where(w => w.Id == newsId).ToList()
                : newsCollection;
            
            // Act
            var actualResult = Sub.Query(new NewsSpecifications
            {
                NewsId = newsId
            });

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Add_InsertNewNews_ReturnNewsWithFilledProperties()
        {
            // Arrange
            var newsId = 10;
            var currentUtc = DateTime.UtcNow;
            var news = new Mock<News>();

            Use<INewsProvider>()
                .Setup(c => c.Create(news.Object))
                .Returns(newsId);

            // Act
            var actualResult = Sub.Add(news.Object);

            // Assert
            Assert.AreEqual(newsId, actualResult.Id);
            Assert.That(currentUtc, Is.LessThan(actualResult.CreatedAtUtc));
        }

        [Test]
        public void Remove_DeleteNewsByValue_CollectionShouldBeUpdated()
        {
            // Arrange
            var newsId = 5;
            var news = new News {Id = newsId};

            // Act
            Sub.Remove(news);

            // Assert
            Use<INewsProvider>().Verify(c => c.Delete(newsId), Times.Once);
        }

        [TestCase(0, "Title", "Text", false)]
        [TestCase(1, "New title", "New text", true)]
        public void Update_SetNewValues_ReturnUpdatedNews(int newsId, string title,
            string text, bool allowComments)
        {
            // Arrange
            var news = new News
            {
                Id = newsId,
                Title = title,
                Text = text,
                AllowComments = allowComments
            };
            var newsCollection = new NewsCollection().Data;
            Use<INewsProvider>()
                .Setup(c => c.GetAll())
                .Returns(newsCollection);

            // Act
            var actualResult = Sub.Update(news);

            // Assert
            Use<INewsProvider>().Verify(c => c.Update(It.IsAny<News>()), Times.Once);
            Assert.AreEqual(newsId, actualResult.Id);
            Assert.AreEqual(title, actualResult.Title);
            Assert.AreEqual(text, actualResult.Text);
            Assert.AreEqual(allowComments, actualResult.AllowComments);
        }
        
        [TestCase(-1)]
        [TestCase(100)]
        public void Update_UseInvalidNewsId_ReturnNull(int newsId)
        {
            // Arrange
            var news = new News
            {
                Id = newsId
            };
            var newsCollection = new NewsCollection().Data;
            Use<INewsProvider>()
                .Setup(c => c.GetAll())
                .Returns(newsCollection);

            // Act
            var actualResult = Sub.Update(news);

            // Assert
            Assert.IsNull(actualResult);
        }
    }
}
