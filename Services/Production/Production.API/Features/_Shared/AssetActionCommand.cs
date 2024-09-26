using Articles.Abstractions;
using Production.Domain.Enums;
using System.Text.Json.Serialization;

namespace Production.API.Features.Shared;

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

public record AsseActiontResponse : IAssetActionResponse
{
		public int Id { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public AssetState State { get; set; }
		public FileDto? File { get; set; }
}

public record FileDto(int FileId, int Version, string FileServerId);