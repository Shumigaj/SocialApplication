using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialApplication.Core.Contracts;
using SocialApplication.Core.Models;
using SocialApplication.Requests;

namespace SocialApplication.Business
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ICommentsProvider _commentsProvider;

        public CommentsRepository(ICommentsProvider commentsProvider)
        {
            _commentsProvider = commentsProvider;
        }

        public async Task<IEnumerable<Comment>> QueryAsync(CommentSpecifications specifications)
        {
            return await Task.Run(() => Query(specifications));
        }

        public IEnumerable<Comment> Query(CommentSpecifications specifications)
        {
            if (specifications == null)
            {
                return null;
            }

            var result = _commentsProvider.GetAll(specifications.NewsId);

            if (specifications.CommentId.HasValue)
            {
                result = result.Where(w => w.Id == specifications.CommentId);
            }

            return result;
        }
        
        public Comment Add(int newsId, Comment comment)
        {
            comment.CreatedAtUtc = DateTime.UtcNow;
            var newCommentId = _commentsProvider.Create(newsId, comment);
            comment.Id = newCommentId;
            return comment;
        }
    }
}
