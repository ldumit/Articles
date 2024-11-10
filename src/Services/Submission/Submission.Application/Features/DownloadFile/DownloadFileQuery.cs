﻿using Articles.MediatR;
using FluentValidation;
using Articles.FluentValidation;

namespace Submission.Application.Features.DownloadFile;

public record DownloadFileQuery(int ArticleId, int AssetId) : ICommand<DownloadFileResponse>;

public record DownloadFileResponse(string FileName, string ContentType, Stream Stream);

public class DownloadFileQuerydValidator : AbstractValidator<DownloadFileQuery>
{
    public DownloadFileQuerydValidator()
    {
        RuleFor(r => r.ArticleId).GreaterThan(0).WithMessageForInvalidId(nameof(DownloadFileQuery.ArticleId));
        RuleFor(r => r.AssetId).GreaterThan(0).WithMessageForInvalidId(nameof(DownloadFileQuery.AssetId));
    }
}