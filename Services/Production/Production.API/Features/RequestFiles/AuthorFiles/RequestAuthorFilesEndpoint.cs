using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.AuthorFiles;

[Authorize(Roles = "TSOF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/author-files:request")]
[Tags("Assets")]

public class RequestAuthorFilesEndpoint(IServiceProvider serviceProvider, AssetRepository assetRepository)
        : RequestFilesEndpointBase<RequestAuthorFilesCommand>(serviceProvider, assetRepository)
{
    //configure
}
