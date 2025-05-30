﻿namespace Review.Application.Dtos;

public record AssetDto(
		int Id, 
		string Name, 
		byte Number, 
		AssetState State,
		AssetType Type,
		AssetCategory CategoryId,
		FileDto File);