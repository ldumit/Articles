using Articles.Abstractions;
using Submission.Application.Dtos;
using Submission.Domain.Enums;
using System.Text.Json.Serialization;

namespace Submission.API.Features.Shared;

//todo - do I need this interface?
public interface IAssetActionCommand : IArticleAction<AssetActionType>
{
}

public interface IAssetActionResponse
{
}


public abstract record AssetActionCommand<TResponse> : AssetCommand<TResponse>, IAssetActionCommand, IRequest<TResponse>
        where TResponse : IAssetActionResponse
{
    internal int FileId { get; set; }
}

public record AssetActionResponse(AssetMinimalDto Asset) : IAssetActionResponse;

public record AssetActionResponse2 : IAssetActionResponse
{
		public int Id { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public AssetState State { get; set; }
		public FileMinimalDto? File { get; set; }
}