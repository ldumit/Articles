﻿using FileStorage.Contracts;

namespace Submission.Domain.Entities;

public partial class File
{
		private File() {/* use factory method*/}

		internal static File CreateFile(UploadResponse uploadResponse, Asset asset, AssetTypeDefinition assetType)
		{
				var fileName = Path.GetFileName(uploadResponse.FilePath);
				var extension = FileExtension.FromFileName(fileName, assetType);

				var file = new File()
				{
						Name = FileName.From(asset, extension),
						Extension = extension,
						OriginalName = fileName,
						Size = uploadResponse.FileSize,
						FileServerId = uploadResponse.FileId
				};
				return file;
		}
}