using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApplication.Core.Models;
using SocialApplication.Requests;

namespace SocialApplication.Business
{
    public interface INewsRepository
    {
        News Add(News news);
        void Remove(News news);
        News Update(News news);
        IEnumerable<News> Query(NewsSpecifications specifications = null);
        Task<IEnumerable<News>> QueryAsync(NewsSpecifications specifications = null);
    }
}
