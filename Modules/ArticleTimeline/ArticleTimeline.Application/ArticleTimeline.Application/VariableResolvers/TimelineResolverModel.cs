using Articles.Abstractions;
using Articles.Security;
using Production.Domain.Enums;

namespace ArticleTimeline.Application.VariableResolvers;

public class TimelineResolverModel
{
    public  AssetType? AssetType { get; set; }
    public ArticleStage NextStage { get; set; }
		public ArticleStage PreviousStage { get; set; }
		
		public int UserId { get; set; }
    public int AssetNumber { get; set; }
    public string Message { get; set; }
    public string UserName { get; set; }
    public UserRoleType UserRole { get; set; }
}
