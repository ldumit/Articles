using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.FinalFiles;

[Authorize(Roles = "POF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/assets/final:request")]
[Tags("Assets")]
public class RequestFinalAssetsEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository)
        : RequestAssetsEndpointBase<RequestFinalAssetsCommand>(articleRepository, assetRepository)
{
    //configure
}
