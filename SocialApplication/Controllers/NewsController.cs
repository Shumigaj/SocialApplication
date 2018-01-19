using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialApplication.Business;
using SocialApplication.Core.Models;
using SocialApplication.Models;
using SocialApplication.Requests;
using SocialApplication.Variables;

namespace SocialApplication.Controllers
{
    [Route("api/v1/[controller]")]
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<News>> Get()
        {
            return await _newsRepository.QueryAsync();
        }
        
        [HttpGet("{id:int:min(0)}", Name = RouteName.GetNews)]
        public async Task<IActionResult> Get(int id)
        {
            var itemsFromStorage = await _newsRepository
                .QueryAsync(new NewsSpecifications {NewsId = id});

            var itemFromStorage = itemsFromStorage?.FirstOrDefault();

            if (itemFromStorage == null)
            {
                return NotFound();
            }

            return new ObjectResult(itemFromStorage);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody]News news)
        {
            if (news == null)
            {
                return BadRequest();
            }
            
            news = _newsRepository.Add(news);

            return CreatedAtRoute(RouteName.GetNews, new { id = news.Id}, news);
        }
        
        [HttpPut]
        public IActionResult Update(int id, [FromBody]News news)
        {
            if (news == null)
            {
                return BadRequest();
            }

            if (id != news.Id)
            {
                var errorDetails = new ErrorDetails("Id parameter must has the same value like id from request body");
                return new BadRequestObjectResult(errorDetails);
            }

            var updatedNews = _newsRepository.Update(news);
            if (updatedNews == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var news = _newsRepository
                .Query(new NewsSpecifications{NewsId = id})
                .SingleOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            try
            {
                _newsRepository.Remove(news);
            }
            catch (Exception e)
            {
                return ResultBuilder.CreateErrorResult(HttpStatusCode.InternalServerError, e.Message);
            }
            
            return new NoContentResult();
        }
    }
}
