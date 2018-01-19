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
    [Route("api/v1/news/{newsId:int:min(0)}/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsController(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Comment>> Get(int newsId)
        {
            return await _commentsRepository.QueryAsync(new CommentSpecifications{NewsId = newsId });
        }
        
        [HttpGet("{id:int:min(0)}", Name = RouteName.GetComments)]
        public async Task<IActionResult> Get(int newsId, int id)
        {
            var itemsFromStorage = await _commentsRepository
                .QueryAsync(new CommentSpecifications { NewsId = newsId, CommentId = id});

            var itemFromStorage = itemsFromStorage?.FirstOrDefault();

            if (itemFromStorage == null)
            {
                return NotFound();
            }

            return new ObjectResult(itemFromStorage);
        }
        
        [HttpPost]
        public IActionResult Create(int newsId, [FromBody]Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            comment = _commentsRepository.Add(newsId, comment);

            if (comment.Id >= 0)
            {
                return CreatedAtRoute(RouteName.GetComments, new {newsId, id = comment.Id}, comment);
            }

            var errorDetails = new ErrorDetails("Comments can be disable for this news or news with provided Id doesn't exists");
            return new BadRequestObjectResult(errorDetails);
        }

        [HttpPut]
        public IActionResult Update(int newsId, int id, [FromBody] Comment comment)
        {
            return ResultBuilder.CreateErrorResult(HttpStatusCode.Forbidden, 
                "Operation wasn't allowed because this can damage the integrity of user correspondence.");
        }

        [HttpDelete]
        public IActionResult Delete(int newsId, int id)
        {
            return ResultBuilder.CreateErrorResult(HttpStatusCode.Forbidden,
                "Operation wasn't allowed because this can damage the integrity of user correspondence.");
        }
    }
}
