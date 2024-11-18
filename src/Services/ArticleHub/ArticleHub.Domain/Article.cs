using Articles.Abstractions.Enums;
using Blocks.Entitities;

namespace ArticleHub.Domain;

public class Article : IEntity
{
    public int Id { get; init; }
    public required string Title { get; set; }
		public required string Doi { get; set; }
		public ArticleStage Stage { get; set; }

		public required virtual int SubmitedById { get; set; }
		public virtual Person SubmitedBy { get; set; } = null!;
		
		public DateTime SubmitedOn { get; set; }
		public DateTime? AcceptedOn { get; set; }
		public DateTime? PublishedOn { get; set; }

		public required int JournalId { get; set; }
		public Journal Journal { get; init; } = null!;

		private readonly List<ArticleContributor> _contributors = new();
		public IReadOnlyCollection<ArticleContributor> Contributors => _contributors.AsReadOnly();
}
