using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
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
    [TestFixture(Category = CategoryName.Comments)]
    public class CommentsControllerTests : AutoMockedSubject<CommentsController>
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public async Task Get_FindCommentById_ReturnComment(int newsId, int commentId)
        {
            // Arrange
            var expectedCommentQuery = new NewsCollection().Data
                .First(w => w.Id == newsId)
                .Comments.Where(w => w.Id == commentId)
                .ToList();

            var expectedComment = expectedCommentQuery.First();

            Use<ICommentsRepository>()
                .Setup(s => s.QueryAsync(It.IsAny<CommentSpecifications>()))
                .ReturnsAsync(expectedCommentQuery);

            // Act
            var actualResult = await Sub.Get(newsId, commentId);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(actualResult);
            var commentFromResponse = (Comment)((ObjectResult) actualResult).Value; 
            Assert.AreEqual(expectedComment, commentFromResponse);
        }

        [TestCase(100, 0)]
        [TestCase(1, 100)]
        public async Task Get_FindNonExistentComment_ReturnNotFound(int newsId, int commentId)
        {
            // Arrange
            var expectedCommentQuery = new NewsCollection().Data
                                           .FirstOrDefault(w => w.Id == newsId)
                                           ?.Comments.Where(w => w.Id == commentId)
                                           .ToList()
                                       ?? new List<Comment>();

            Use<ICommentsRepository>()
                .Setup(s => s.QueryAsync(It.IsAny<CommentSpecifications>()))
                .ReturnsAsync(expectedCommentQuery);
            
            // Act
            var actualResult = await Sub.Get(newsId, commentId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actualResult);
        }

        [Test]
        public void Create_AddCommentToNews_ReturnCommentWithId()
        {
            // Arrange
            var newsId = 0;
            var commentId = 10;
            var comment = new Comment
            {
                Id = commentId
            };
            Use<ICommentsRepository>()
                .Setup(s => s.Add(newsId, comment))
                .Returns(comment);

            // Act
            var actualResult = Sub.Create(newsId, comment);

            // Assert
            Assert.IsInstanceOf<CreatedAtRouteResult>(actualResult);
            var createdAtRouteResult = (CreatedAtRouteResult) actualResult;
            var commentFromResult = (Comment)createdAtRouteResult.Value;
            Assert.AreEqual(comment, commentFromResult);
            Assert.AreEqual(createdAtRouteResult.RouteName, RouteName.GetComments);
            CollectionAssert.AreEqual(new object[]{ newsId, commentId },  createdAtRouteResult.RouteValues.Values);
        }

        [Test]
        public void Create_CommentCanNotBeNull_ReturnBadRequest()
        {
            // Act
            var actualResult = Sub.Create(0, null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(actualResult);
        }

        [Test]
        public void Create_DontAddCommentIfNotAllowed_ReturnBadRequest()
        {
            // Arrange
            var newsId = 0;
            var comment = new Comment
            {
                Id = -1
            };
            Use<ICommentsRepository>()
                .Setup(s => s.Add(newsId, comment))
                .Returns(comment);

            // Act
            var actualResult = Sub.Create(newsId, comment);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(actualResult);
            var badRequestObjectResult = (BadRequestObjectResult)actualResult;
            Assert.AreEqual(badRequestObjectResult.StatusCode, Convert.ToInt32(HttpStatusCode.BadRequest));
        }

        [Test]
        public void Update_CallEndpoint_ShouldProhibitProcessing()
        {
            // Arrange
            var expectedResult = ResultBuilder.CreateErrorResult(HttpStatusCode.Forbidden, "Forbidden");

            // Act
            var actualResult = (ObjectResult)Sub.Update(0, 0, null);

            // Assert
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }

        [Test]
        public void Delete_CallEndpoint_ShouldProhibitProcessing()
        {
            // Arrange
            var expectedResult = ResultBuilder.CreateErrorResult(HttpStatusCode.Forbidden, "Forbidden");

            // Act
            var actualResult = (ObjectResult)Sub.Delete(0, 0);

            // Assert
            Assert.AreEqual(expectedResult.StatusCode, actualResult.StatusCode);
        }
    }
}
