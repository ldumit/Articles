﻿using FileStorage.Contracts;
using Production.Domain.ValueObjects;

namespace Production.Domain.Entities;

public partial class File
{
    private File(){/* use factory method*/}

    public static File CreateFile(UploadResponse uploadResponse, Asset asset, AssetTypeDefinition assetTypeDefinition)
		{
				var fileName = Path.GetFileName(uploadResponse.FilePath);
				var extension = FileExtension.FromFileName(fileName, assetTypeDefinition);

				var file = new File()
				{
						Name = FileName.From(asset, extension),
						Version = FileVersion.FromAsset(asset),
						Extension = extension,
						OriginalName = fileName,
						Size = uploadResponse.FileSize,
						FileServerId = uploadResponse.FilePath
				};
				return file;
		}
}
