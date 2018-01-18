using Microsoft.AspNetCore.Mvc;

namespace SocialApplication.Controllers
{
    [Route("api/v1/news/{newsId:int:min(0)}/[controller]")]
    public class CommentsController : Controller
    {

    }
}
