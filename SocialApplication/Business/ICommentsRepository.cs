using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApplication.Core.Models;
using SocialApplication.Requests;

namespace SocialApplication.Business
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> QueryAsync(CommentSpecifications specifications);

        IEnumerable<Comment> Query(CommentSpecifications specifications);

        Comment Add(int newsId, Comment comment);
    }
}
