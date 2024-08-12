using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Production.Application;
using Production.Persistence.Repositories;

namespace Production.API.Features.UploadAuthorsProof;

[Authorize(AuthenticationSchemes = "Cookie")]
[Route("api/articles/{articleId:int}")]
[ApiController]
public class AssignTypesetterEndpoint(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Uploads new version of Author's Proof Asset.
    /// </summary>
    /// <param name="request">The upload command.</param>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UploadFileResponse))]
    [Authorize(Roles = "tsof,pof")]
    [HttpPost("authors-proof:upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAuthorsProof([FromForm] UploadAuthorsProofCommand command)
    {
        return Ok(await base.SendAsync(command));
    }
}

public record UploadAuthorsProofCommand : UploadFileCommand
{
}

public class UploadAuthorsProofCommandValidator : UploadFileCommandValidator<UploadAuthorsProofCommand>
{
    public UploadAuthorsProofCommandValidator(AuthorProofAssetProvider assetProvider, ArticleRepository articleRepository, AssetRepository assetRepository) : base(assetProvider, articleRepository, assetRepository)
    {
    }
}
