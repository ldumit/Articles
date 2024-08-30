using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Production.API.Features;

public record UploadFinalFileCommand : UploadFileCommand
{
}
public record UploadAuthorsProofCommand : UploadFileCommand
{
}
public record UploadPublisherProofCommand : UploadFileCommand
{
}
public record UploadXmlCommand : UploadFileCommand
{
}
public record UploadHtmlCommand : UploadFileCommand
{
}
public record UploadEpubCommand : UploadFileCommand
{
}
public record UploadCorrectionFileCommand : UploadFileCommand
{
}
public record UploadAuthorCorrectionFileCommand : UploadFileCommand
{
		/// <summary>
		/// The asset number of the file.
		/// </summary>
		[Required]
		public byte AssetNumber { get; set; }

		internal override byte GetAssetNumber() => AssetNumber;
}

public record FinishUploadBatchCommand : IRequest<FinishUploadBatchResponse>
{
		/// <summary>
		/// The article Id.
		/// </summary>
		[FromRoute, Required]
		public int ArticleId { get; set; }

		/// <summary>
		/// The batch id for uplaoding multiple files in a batch.
		/// </summary>
		[FromRoute, Required]
		public Guid BatchId { get; set; }
}

public record FinishUploadBatchResponse
{
		/// <summary>
		/// Returns the list of details of the uploaded files.
		/// </summary>
		public IList<UploadFileResponse> UploadFileResponses { get; set; }
}
