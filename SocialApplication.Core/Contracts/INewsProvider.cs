using SocialApplication.Core.Models;
using System.Collections.Generic;

namespace SocialApplication.Core.Contracts
{
    public interface INewsProvider
    {
        int Create(News news);
        void Delete(int newsId);
        void Update(News updatedNews);
        IEnumerable<News> GetAll();
    }
}