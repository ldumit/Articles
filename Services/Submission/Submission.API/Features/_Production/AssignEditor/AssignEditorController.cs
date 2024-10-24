using Articles.Abstractions;
using Azure;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Submission.API.Features.Shared;
using Submission.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;

///using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Submission.API.Features.AssignEditor;

[Authorize(AuthenticationSchemes = "Cookie")]
[Route("api/articles/{articleId:int}")]
[ApiController]
[Tags("Articles")]
public class AssignEditorController(IMediator mediator) : ApiControllerBase(mediator)
{
    //[Authorize(Roles = "pof")]
    //[HttpPut("{articleId:int}/typesetter")]
    //public async Task<IActionResult> AssignTypesetter(AssignTypesetterCommand command)
    //{
    //    return Ok(await base.HandleAsync(command));
    //}

		[Authorize(Roles = "pof")]
		[Microsoft.AspNetCore.Mvc.HttpPut("/api/articles/{articleId:int}/editor")]
		public async Task<IActionResult> AssignEditor(AssignEditorCommand command)
		{
				return Ok(await base.HandleAsync(command));
		}

		//		[Authorize(Roles = "pof")]
		//		[HttpPut("{articleId:int}/editor/{editorId:int}")]
		//		public async Task<IActionResult> AssignEditor3([FromRoute] int articleId, [FromRoute] int editorId)
		//		public async Task<IActionResult> AssignEditor2([FromRoute] EditorDto editorDto)
		//		public async Task<IActionResult> AssignEditor3([FromRoute] int articleId, [FromRoute] int editorId)
		//		{
		//				return Ok(await base.HandleAsync(new AssignEditorCommand() {
		//						ArticleId = articleId, 
		//						EditorId = editorDto.EditorId, 
		//						Comment = editorDto.Comment}));
		//		}


		//[Authorize(Roles = "pof")]
		//[HttpPut("articles/{articleId:int}/editor")]
		//public async Task<IActionResult> AssignEditor([FromRoute] int articleId, [FromBody] EditorDto editorDto)
		//{
		//		return Ok(await base.HandleAsync(new AssignEditorCommand()
		//		{
		//				ArticleId = articleId,
		//				EditorId = editorDto.EditorId,
		//				NotifyUser = editorDto.NotifyUser,
		//				Comment = editorDto.Comment
		//		}));
		//}

}

public record AssignEditorCommand : IRequest<ArticleCommandResponse>
{
		[FromRoute] public required int ArticleId { get; set; }
		[Microsoft.AspNetCore.Mvc.FromBody] public EditorDto Editor { get; init; }
}

public record AssignEditorCommand2 
{
    [FromRoute] public required int ArticleId { get; set; }
		[FromRoute] public required int EditorId { get; init; }
		[Microsoft.AspNetCore.Mvc.FromBody] public bool NotifyUser { get; init; }
		[Microsoft.AspNetCore.Mvc.FromBody] public string Comment { get; init; }
}

public record EditorDto2
{
    public required int ArticleId { get; set; }
    public required int EditorId { get; init; }
}

public record EditorDto
{
		public required int EditorId { get; init; }
		public bool NotifyUser { get; init; }
		public string Comment { get; init; }
}

