using SocialApplication.Core.Models;
using System.Collections.Generic;

namespace SocialApplication.Core.Contracts
{
    public interface INewsProvider
    {
        int Create(News post);
        void Delete(int postId);
        void Update(News post);
        IEnumerable<News> GetAll();
    }
}