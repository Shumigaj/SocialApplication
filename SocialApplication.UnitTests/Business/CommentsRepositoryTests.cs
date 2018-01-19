using System;
using System.Collections.Generic;
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
    [TestFixture(Category = CategoryName.Comments)]
    public class CommentsRepositoryTests : AutoMockedSubject<CommentsRepository>
    {
        [TestCase(-1, -1)]
        [TestCase(1, 0)]
        public void Add_InsertNewComment_ReturnCommentWithFilledProperties(int newsId, int commentId)
        {
            // Arrange
            var comment = new Comment();
            var currentUtc = DateTime.UtcNow;
            Use<ICommentsProvider>()
                .Setup(c => c.Create(newsId, comment))
                .Returns(commentId);

            // Act
            var actualResult = Sub.Add(newsId, comment);

            // Assert
            Assert.AreEqual(commentId, actualResult.Id);
            Assert.That(currentUtc, Is.LessThan(actualResult.CreatedAtUtc));
        }

        [Test]
        public void Query_SpecificationIsNull_ReturnNull()
        {
            // Act
            var actualResult = Sub.Query(null);

            // Assert
            Assert.IsNull(actualResult);
        }

        [TestCase(0, null, 2)]
        [TestCase(0, 0, 1)]
        [TestCase(100, 0, 0)]
        public void Query_ApplyFilter_RenurnFilteredResult(int newsId, int? commentId, int commentsCount)
        {
            // Arrange
            var commentsCollection = new NewsCollection().Data.FirstOrDefault(w => w.Id == newsId)?.Comments
                                 ?? new List<Comment>();

            var expectedResult = commentId.HasValue
                ? commentsCollection.Where(w => w.Id == commentId).ToList()
                : commentsCollection;

            Use<ICommentsProvider>()
                .Setup(c => c.GetAll(newsId))
                .Returns(commentsCollection);

            // Act
            var actualResult = Sub.Query(new CommentSpecifications
            {
                NewsId = newsId,
                CommentId = commentId
            });

            // Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
