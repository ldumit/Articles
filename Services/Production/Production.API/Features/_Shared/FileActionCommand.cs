using Articles.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Production.API.Features.Shared;

public interface IAssetActionCommand : IArticleAction<AssetActionType>
{
}

public interface IFileActionResponse
{
}


public abstract record FileActionCommand<TResponse> : AssetCommand<TResponse>, IAssetActionCommand, IRequest<TResponse>
        where TResponse : IFileActionResponse
{
    internal int FileId { get; set; }
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