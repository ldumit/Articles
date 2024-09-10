using Articles.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Production.Domain.Enums;
using System.Text.Json.Serialization;

namespace Production.API.Features.AssignTypesetter;

public record AssignTypesetterCommand : Domain.IArticleAction
{
		[FromRoute]
		public int ArticleId { get; init; }
		[FromRoute]
		public int TypesetterId { get; init; }
		[FastEndpoints.FromBody]
		public CommandBody Body { get; init; }

		//todo check why the FromClaim doesn't work
		//[FromClaim(JwtRegisteredClaimNames.Sub)]
		//[JsonIgnore]
		int IArticleAction.UserId { get; set; }

		string IArticleAction.Comment => Body.Comment;

		ActionType Domain.IArticleAction.ActionType => ActionType.AssignTypesetter;
		//int IArticleAction.UserId { get; set; }
}