﻿using Articles.Abstractions.Enums;
using Production.Domain.Enums;

namespace Production.Application.Dtos;

public record AssetDto(
		int Id, 
		string Name, 
		byte Number, 
		AssetState State,
		AssetType Type,
		AssetCategory CategoryId,
		IReadOnlyList<FileDto> Files);