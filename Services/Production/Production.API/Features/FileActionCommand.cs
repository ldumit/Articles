using Articles.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Production.API.Features;


public abstract record AssetActionCommand<TResponse> : ArticleCommand2<TResponse>
{
		public int AssetId { get; set; }
}

public abstract record AssetActionCommand : ArticleCommand2<AssetActionResponse>
{
		public int AssetId { get; set; }
}



public interface IFileActionCommand : IArticleAction
{
}

public interface IFileActionResponse
{
}


public abstract record FileActionCommand<TResponse> : ArticleCommand<TResponse>, IFileActionCommand, IRequest<TResponse>
		where TResponse : IFileActionResponse
{
		internal int FileId { get; set; }
}

public abstract record FileActionWithBodyCommand : FileActionCommand<AssetActionResponse>
{
		/// <summary>
		/// The AssetId.
		/// </summary>
		[FromRoute]
		[Required]
		public int AssetId { get; set; }

		/// <summary>
		/// The file action comment.
		/// </summary>
		[FromBody]
		public FileActionBody Body { get; set; }

		protected override string GetActionComment() => Body?.Comment;
}

public record FileActionBody : CommandBody
{
}

public class AssetActionResponse : IFileActionResponse
{
		/// <summary>
		/// Returns the assetId of the uploaded file
		/// </summary>
		public int AssetId { get; internal set; }
		/// <summary>
		/// Returns the fileId of the uploaded file.
		/// </summary>
		public int? FileId { get; internal set; }
		/// <summary>
		/// Returns the fileServerId of the uploaded file
		/// </summary>
		public string FileServerId { get; internal set; }
		/// <summary>
		/// Returns the version of the uploaded file.
		/// </summary>
		public int Version { get; internal set; }
}