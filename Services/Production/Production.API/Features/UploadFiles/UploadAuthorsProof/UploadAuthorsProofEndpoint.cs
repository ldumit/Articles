using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Application;
using Production.Persistence.Repositories;

namespace Production.API.Features.UploadFiles.UploadAuthorsProof;

[Authorize(AuthenticationSchemes = "Cookie")]
[Route("api/articles/{articleId:int}")]
[ApiController]
public class UploadAuthorsProofEndpoint(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Uploads new version of Author's Proof Asset.
    /// </summary>
    /// <param name="request">The upload command.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResponse))]
    [Authorize(Roles = "tsof,pof")]
    [HttpPost("authors-proof:upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAuthorsProof([FromForm] UploadAuthorsProofCommand command)
    {
        return Ok(await SendAsync(command));
    }
}

public record UploadAuthorsProofCommand : UploadFileCommand
{
}

//public class UploadAuthorsProofCommandValidator : UploadFileCommandValidator<UploadAuthorsProofCommand>
//{
//    public UploadAuthorsProofCommandValidator(HtmlAssetProvider assetProvider, ArticleRepository articleRepository, AssetRepository assetRepository) : base(assetProvider, articleRepository, assetRepository)
//    {
//    }
//}
