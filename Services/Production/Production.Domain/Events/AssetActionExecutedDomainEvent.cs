using Articles.Abstractions;
using Production.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Domain.Events
{
		public record AssetActionExecutedDomainEvent(IArticleAction<AssetActionType> action, AssetType AssetType, int AssetNumber, Entities.File? File)
		: DomainEvent<AssetActionType>(action.ArticleId, action.ActionType, action.UserId, action.Comment)
		{
		}
}
