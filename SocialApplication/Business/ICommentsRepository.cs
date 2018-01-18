using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApplication.Core.Models;
using SocialApplication.Requests;

namespace SocialApplication.Business
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> QueryAsync(CommentSpecifications specifications = null);

        IEnumerable<Comment> Query(CommentSpecifications specifications = null);

        Comment Add(int newsId, Comment comment);
    }
}
