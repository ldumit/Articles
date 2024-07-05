using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Production.Domain.Enums;

namespace Production.API.Features.AssignTypesetter;

[Authorize(AuthenticationSchemes = "Cookie")]
[Route("api/articles/{articleId:int}")]
[ApiController]
public class AssignTypesetterEndpoint(IMediator mediator) : ApiControllerBase(mediator)
{
    [Authorize(Roles = "pof")]
    [HttpPut("{articleId:int}/typesetter")]
    public async Task<IActionResult> AssignTypesetter(AssignTypesetterCommand command)
    {
        return Ok(await base.HandleAsync(command));
    }
}

public record AssignTypesetterCommand : ArticleActionCommand<AssignTypesetterCommandBody, ArticleCommandResponse>
{
    internal override FileActionType ActionType => FileActionType.AssignTypesetter;
}

public record AssignTypesetterCommandBody : CommandBody
{
    public int UserId { get; set; }
}
