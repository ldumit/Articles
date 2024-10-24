using FluentValidation;
using Submission.API.Features.Shared;

namespace Submission.API.Features.DownloadFile;

public record DownloadFileCommand(int ArticleId, int FileId);

public class DownloadFileCommandValidator : BaseValidator<DownloadFileCommand>
{
    public DownloadFileCommandValidator()
    {
        RuleFor(r => r.ArticleId).GreaterThan(0);
        RuleFor(r => r.FileId).GreaterThan(0);
    }
}