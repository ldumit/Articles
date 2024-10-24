using Submission.Domain.Enums;

namespace Submission.Application.Dtos;

public record AssetMinimalDto(int Id, AssetState State, FileMinimalDto? File);
