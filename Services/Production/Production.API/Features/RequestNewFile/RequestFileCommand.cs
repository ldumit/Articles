using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Production.API.Features;

public record RequestFileCommand : ArticleCommand2<RequestFileCommandResponse>
{
		public AssetType AssetType { get; set; }
		public override ActionType ActionType => ActionType.RequestNew;

		public AssetRequest[] AssetRequests { get; set; }
		public class AssetRequest
		{
				public AssetType AssetType { get; set; }
				/// <summary>
				/// The asset number of the file.
				/// </summary>
				[Required]
				public byte AssetNumber { get; set; }
		}
}

//todo do I need authorsProof? it might be confusing, the final file should be enough
public record RequestAuthorProofCommand : RequestFileCommand
{
}

public record RequestFinalFileCommand : RequestFileCommand
{
}

public record RequestAuthorFileCommand : RequestFileCommand
{
    public int AssetNumber { get; init; }
}

public class RequestFileCommandResponse
{
		/// <summary>
		/// Returns the details of files requested for new versions
		/// </summary>

		public IEnumerable<UploadFileResponse> Assets { get; set; }
}