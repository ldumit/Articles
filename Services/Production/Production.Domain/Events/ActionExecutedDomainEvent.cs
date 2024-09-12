using Production.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Domain.Events
{
		public record ActionExecutedDomainEvent(IArticleAction action, AssetType AssetType, int AssetNumber, Entities.File? File)
		: DomainEvent(action.ArticleId, action.ActionType, action.UserId, action.Comment)
		{
		}
}
