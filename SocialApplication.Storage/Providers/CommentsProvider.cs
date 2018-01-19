using System.Collections.Generic;
using System.Linq;
using SocialApplication.Core.Contracts;
using SocialApplication.Core.Models;
using SocialApplication.Storage.Data;

namespace SocialApplication.Storage.Providers
{
    public class CommentsProvider : ICommentsProvider
    {
        private readonly IList<News> _newsCollection;

        public CommentsProvider()
        {
            _newsCollection = NewsCollectionStorage.Data;
        }

        public IEnumerable<Comment> GetAll(int newsId)
        {
            var news = _newsCollection.FirstOrDefault(w => w.Id == newsId);
            return news == null ? Enumerable.Empty<Comment>() : news.Comments;
        }

        public int Create(int newsId, Comment comment)
        {
            var news = _newsCollection.FirstOrDefault(w => w.Id == newsId);
            if (news == null || !news.AllowComments)
            {
                return -1;
            }

            comment.Id = news.Comments.Count > 0 
                ? news.Comments.Max(s => s.Id) + 1
                : 0;
            news.Comments.Add(comment);
            return comment.Id;
        }
    }
}
