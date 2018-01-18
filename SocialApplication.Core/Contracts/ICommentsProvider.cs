using System.Collections.Generic;
using SocialApplication.Core.Models;

namespace SocialApplication.Core.Contracts
{
    public interface ICommentsProvider
    {
        IEnumerable<Comment> GetAll(int newsId);

        int Create(int newsId, Comment comment);
    }
}
