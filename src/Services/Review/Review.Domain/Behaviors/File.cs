using Articles.Abstractions.Events.Dtos;
using FileStorage.Contracts;

namespace Review.Domain.Entities;

public partial class File
{
		private File() {/* use factory method*/}

		public static File CreateFile(UploadResponse uploadResponse, Asset asset, AssetTypeDefinition assetType)
		{
				var fileName = Path.GetFileName(uploadResponse.FilePath);
				var extension = FileExtension.FromFileName(fileName, assetType);

				var file = new File()
				{
						Name = FileName.From(asset, extension),
						Extension = extension,
						OriginalName = fileName,
						Size = uploadResponse.FileSize,
						FileServerId = uploadResponse.FilePath
				};
				return file;
		}

		public static File CreateFile(FileDto fileDto, AssetTypeDefinition assetType)
		{
				var extension = FileExtension.FromFileName(fileDto.OriginalName, assetType);

				var file = new File()
				{
						Name = new FileName(fileDto.Name),
						Extension = extension,
						OriginalName = fileDto.OriginalName,
						Size = fileDto.Size,
						FileServerId = fileDto.FileServerId
				};
				return file;
		}
}