using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Production.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookie")]
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        //[Authorize(Roles = "pof,tsof,aut,eof")]
        //[HttpGet("{articleId:int}/summary")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleSummaryQueryResponse))]
        //public async Task<IActionResult> Summary(ArticleSummaryQuery query)
        //{
        //    return Ok(await base.HandleAsync(query));
        //}
    }
}
