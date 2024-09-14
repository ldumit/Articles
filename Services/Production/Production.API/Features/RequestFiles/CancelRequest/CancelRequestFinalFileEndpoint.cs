using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.API.Features.Shared;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.Cancel;

[Authorize(Roles = "POF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/final-file:cancel-request")]
[Tags("Assets")]
public class CancelRequestFinalFileEndpoint(IServiceProvider serviceProvider, AssetRepository _assetRepository)
        : BaseEndpoint<CancelRequestFinalFilesCommand, RequestFilesCommandResponse>(serviceProvider)
{
    public async override Task HandleAsync(CancelRequestFinalFilesCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);

        foreach (var assetRequest in command.AssetRequests)
        {
            var asset = article.Assets.SingleOrDefault(a => a.TypeCode == assetRequest.AssetType);
            if (asset?.Status == AssetStatus.Requested)
            {
                asset.SetStatus(AssetStatus.Requested, command);
            }
        }
        await _assetRepository.SaveChangesAsync();

        await SendAsync(new RequestFilesCommandResponse
        {

        });
    }
}
