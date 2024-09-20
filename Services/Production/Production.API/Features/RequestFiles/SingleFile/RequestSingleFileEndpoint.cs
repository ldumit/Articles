using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.Shared;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.SingleFile;

[Authorize(Roles = "TSOF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/single-file:request")]
[Tags("Assets")]
public class RequestSingleFileEndpoint(IServiceProvider serviceProvider, AssetRepository _assetRepository)
        : BaseEndpoint<RequestSingleFileCommand, AssetActionResponse>(serviceProvider)
{
    public async override Task HandleAsync(RequestSingleFileCommand command, CancellationToken cancellationToken)
    {
        var asset = await _assetRepository.GetByTypeAndNumber(command.ArticleId, command.AssetType, command.AssetNumber);

        asset.SetStatus(AssetState.Requested, command);

        await _assetRepository.SaveChangesAsync();

        await SendAsync(new AssetActionResponse
        {
            AssetId = asset.Id,
            FileId = asset.CurrentFile?.Id,
            FileServerId = asset.CurrentFile.FileServerId,
            Version = asset.CurrentFile.Version
        });
    }
}
