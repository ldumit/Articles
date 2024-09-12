using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.FinalFiles;

[Authorize(Roles = "POF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/final-files:request")]
[Tags("Assets")]
public class RequestFinalFilesEndpoint(IServiceProvider serviceProvider, AssetRepository assetRepository)
        : RequestFilesEndpointBase<RequestFinalFilesCommand>(serviceProvider, assetRepository)
{
    //configure
}
