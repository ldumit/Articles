using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.Application.StateMachines;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.AuthorFiles;

[Authorize(Roles = "TSOF")]
[HttpPut("articles/{articleId:int}/assets/supplementary:request")]
[Tags("Assets")]

public class RequestSupplementaryAssetsEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, AssetStateMachineFactory factory)
    : RequestAssetsEndpointBase<RequestSupplementaryAssetsCommand>(articleRepository, assetRepository, factory)
{
}
