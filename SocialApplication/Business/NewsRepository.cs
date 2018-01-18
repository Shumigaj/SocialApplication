using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public News Add(News news)
        {
            news.CreatedAtUtc = DateTime.UtcNow;
            var newNewsId = _newsProvider.Create(news);
            news.Id = newNewsId;
            return news;
        }

        public void Remove(News news)
        {
            _newsProvider.Delete(news.Id);
        }

        public News Update(News news)
        {
            var item = _newsProvider.GetAll().FirstOrDefault(w => w.Id == news.Id);
            if (item == null)
            {
                return null;
            }

            item.Title = news.Title;
            item.Text = news.Text;
            item.AllowComments = news.AllowComments;
            item.ModifiedAtUtc = DateTime.UtcNow;

            _newsProvider.Update(item);

            return item;
        }

        public async Task<IEnumerable<News>> QueryAsync(NewsSpecifications specifications = null)
        {
            return await Task.Run(() => Query(specifications));
        }

        public IEnumerable<News> Query(NewsSpecifications specifications = null)
        {
            var result = _newsProvider.GetAll();
            if (specifications == null)
            {
                return result;
            }

            if (specifications.NewsId.HasValue)
            {
                result = result.Where(w => w.Id == specifications.NewsId);
            }

            return result;
        }
    }
}
