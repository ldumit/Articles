using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Submission.API.Features.RequestFiles.Shared;
using Submission.Application.StateMachines;
using Submission.Persistence.Repositories;

namespace Submission.API.Features.RequestFiles.AuthorFiles;

[Authorize(Roles = "TSOF")]
[HttpPut("articles/{articleId:int}/assets/supplementary:request")]
[Tags("Assets")]
public class RequestSupplementaryAssetsEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, AssetStateMachineFactory factory)
    : RequestAssetsEndpointBase<RequestSupplementaryAssetsCommand>(articleRepository, assetRepository, factory)
{
}
