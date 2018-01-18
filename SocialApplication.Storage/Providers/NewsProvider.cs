using System.Collections.Generic;
using System.Linq;
using SocialApplication.Core.Contracts;
using SocialApplication.Core.Models;
using SocialApplication.Storage.Data;

namespace SocialApplication.Storage.Providers
{
    public class NewsProvider : INewsProvider
    {
        private readonly IList<News> _newsCollection;
        private int NextNewsId { get; set; }

        public NewsProvider()
        {
            _newsCollection = new NewsCollectionGenerator().CreateApplicationContent();
            NextNewsId = _newsCollection.Max(w => w.Id) + 1;
        }

        public int Create(News news)
        {
            news.Id = NextNewsId;
            _newsCollection.Add(news);
            NextNewsId++;
            return news.Id;
        }

        public void Delete(int newsId)
        {
            var postToRemove = _newsCollection.SingleOrDefault(r => r.Id == newsId);
            if (postToRemove == null)
            {
                return;
            }

            _newsCollection.Remove(postToRemove);
        }

        public void Update(News updatedNews)
        {
            var oldItem = GetAll().First(w => w.Id == updatedNews.Id);
            var itemIndex = _newsCollection.IndexOf(oldItem);
            _newsCollection[itemIndex] = updatedNews;
        }

        public IEnumerable<News> GetAll()
        {
            return _newsCollection;
        }
    }
}
