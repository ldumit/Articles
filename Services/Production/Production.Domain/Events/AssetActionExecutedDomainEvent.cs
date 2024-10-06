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
		: DomainEvent(action.ArticleId, action.Action, action.CreatedById, action.Comment)
		{
		}
}
