using System.Collections.Generic;
using SocialApplication.Core.Contracts;
using SocialApplication.Core.Models;
using SocialApplication.Requests;

namespace SocialApplication.Business
{
    internal class NewsRepository : INewsRepository
    {
        private readonly INewsProvider _newsProvider;

        public NewsRepository(INewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        public void AddPost(News post)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<News> Query(NewsSpecifications specifications)
        {
            throw new System.NotImplementedException();
        }

        public void RemovePost(News post)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePost(News post)
        {
            throw new System.NotImplementedException();
        }
    }
}
