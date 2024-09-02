using Microsoft.AspNetCore.Mvc;
using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Production.API.Features;

public interface IFileActionCommand : IArticleCommand
{
}

public interface IFileActionResponse
{
}


public abstract record FileActionCommand<TResponse> : ArticleActionCommand<TResponse>, IFileActionCommand, IRequest<TResponse>
		where TResponse : IFileActionResponse
{
		internal int FileId { get; set; }
}

public abstract record FileActionWithBodyCommand : FileActionCommand<FileActionResponse>
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

		internal override string ActionComment => Body?.Comment;
		internal override DiscussionType DiscussionGroupType => Body.DiscussionType;
}

public record FileActionBody : CommandBody
{
}

public class FileActionResponse : IFileActionResponse
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