using Articles.Abstractions;
using Articles.Entitities;
using Articles.Security;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class Timeline : AggregateEntity
{
    public int ArticleId { get; set; }
    public ArticleStage CurrentStage { get; set; }
		public ArticleStage PreviousStage { get; set; }
		public SourceType SourceType { get; set; }
    public string SourceId { get; set; } = null!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int? FileId { get; set; }
    //public UserRoleType RoleType { get; set; }
    public int TemplateId { get; set; }
    public TimelineTemplate Template { get; set; } = default!;

		//		public int? MessageId { get; set; }

		//public virtual Article? Article { get; set; }
		//public virtual File? File { get; set; }
		//public virtual Message? Message { get; set; }
}
