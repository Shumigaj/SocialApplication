using System.Collections.Generic;
using SocialApplication.Core.Models;
using SocialApplication.Requests;

namespace SocialApplication.Business
{
    public interface INewsRepository
    {
        void AddPost(News post);
        void RemovePost(News post);
        void UpdatePost(News post);
        IEnumerable<News> Query(NewsSpecifications specifications);
    }
}
