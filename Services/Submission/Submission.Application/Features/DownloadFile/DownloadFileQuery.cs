using Articles.MediatR;
using FluentValidation;
using Submission.Application.Features.Shared;

namespace Submission.Application.Features.DownloadFile;

public record DownloadFileQuery(int ArticleId, int AssetId) : ICommand<DownloadFileResponse>;

public record DownloadFileResponse(string FileName, string ContentType, Stream Stream);

public class DownloadFileQuerydValidator : BaseValidator<DownloadFileQuery>
{
    public DownloadFileQuerydValidator()
    {
        RuleFor(r => r.ArticleId).GreaterThan(0);
        RuleFor(r => r.AssetId).GreaterThan(0);
    }
}