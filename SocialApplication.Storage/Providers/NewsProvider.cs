using System.Collections.Generic;
using System.Linq;
using SocialApplication.Core.Contracts;
using SocialApplication.Core.Models;
using SocialApplication.Storage.Data;

namespace SocialApplication.Storage.Providers
{
    public class NewsProvider : INewsProvider
    {
        private readonly IList<News> _postsCollection;

        public NewsProvider()
        {
            _postsCollection = new NewsCollectionGenerator().CreateApplicationContent();
        }

        public int Create(News post)
        {
            post.Id = _postsCollection.Max(w => w.Id) + 1;
            _postsCollection.Add(post);
            return post.Id;
        }

        public void Delete(int postId)
        {
            var postToRemove = _postsCollection.SingleOrDefault(r => r.Id == postId);
            if (postToRemove == null)
            {
                return;
            }

            _postsCollection.Remove(postToRemove);
        }

        public void Update(News post)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<News> GetAll()
        {
            return _postsCollection;
        }
    }
}
