using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.Application.StateMachines;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.FinalFiles;

[Authorize(Roles = Role.POF)]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/assets/final:request")]
[Tags("Assets")]
public class RequestFinalAssetsEndpoint(ArticleRepository articleRepository, AssetTypeRepository assetTypeRepository, AssetStateMachineFactory factory)
        : RequestAssetsEndpointBase<RequestFinalAssetsCommand>(articleRepository, assetTypeRepository, factory)
{
    //configure
}
