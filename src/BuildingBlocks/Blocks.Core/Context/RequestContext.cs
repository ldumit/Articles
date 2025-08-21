namespace Blocks.Core.Context;

public class RequestContext
{
		public string? CorrelationId { get; set; }
		public bool IsFileTransfer => IsUpload || IsDownload;

		public bool IsUpload { get; set; } = false;
		public bool IsDownload { get; set; } = false;
}
