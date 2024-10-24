using Submission.Domain.Enums;

namespace Submission.API.Features.UploadArticle;

public record CreateArticleCommand(int JournalId, string Title, ArticleType Type, string ScopeStatement);
