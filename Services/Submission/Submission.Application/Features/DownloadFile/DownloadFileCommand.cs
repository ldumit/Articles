using Articles.MediatR;
using FluentValidation;
using Submission.Application.Features.Shared;

namespace Submission.Application.Features.DownloadFile;

public record DownloadFileCommand(int ArticleId, int FileId) : ICommand;

public class DownloadFileCommandValidator : BaseValidator<DownloadFileCommand>
{
    public DownloadFileCommandValidator()
    {
        RuleFor(r => r.ArticleId).GreaterThan(0);
        RuleFor(r => r.FileId).GreaterThan(0);
    }
}