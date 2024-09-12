using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.Shared;

public class RequestFilesEndpointBase<TCommand>(IServiceProvider serviceProvider, AssetRepository _assetRepository)
        : BaseEndpoint<TCommand, RequestFilesCommandResponse>(serviceProvider)
        where TCommand : RequestMultipleFilesCommand
{
    public async override Task HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);
        var response = new RequestFilesCommandResponse();
        foreach (var assetRequest in command.AssetRequests)
        {
            var asset = article.Assets
                    .SingleOrDefault(asset => asset.TypeCode == assetRequest.AssetType && asset.AssetNumber == assetRequest.AssetNumber);
            if (asset != null)
            {
                asset.SetStatus(AssetStatus.Requested, command);

                response.Assets.Add(_mapper.Map<UploadFileResponse>(asset.LatestFile));
            }
        }
        await _assetRepository.SaveChangesAsync();

        await SendAsync(response);
    }
}
