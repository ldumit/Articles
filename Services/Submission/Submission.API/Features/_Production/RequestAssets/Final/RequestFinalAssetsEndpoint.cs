using Articles.Security;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Submission.API.Features.RequestFiles.Shared;
using Submission.Application.StateMachines;
using Submission.Persistence.Repositories;

namespace Submission.API.Features.RequestFiles.FinalFiles;

[Authorize(Roles = Role.POF)]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/assets/final:request")]
[Tags("Assets")]
public class RequestFinalAssetsEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, AssetStateMachineFactory factory)
        : RequestAssetsEndpointBase<RequestFinalAssetsCommand>(articleRepository, assetRepository, factory)
{
    //configure
}
