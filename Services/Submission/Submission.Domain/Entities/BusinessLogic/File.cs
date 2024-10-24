using FileStorage.Contracts;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class File
{
    private File(){/* use factory method*/}

    public static File CreateFile(UploadResponse uploadResponse, Asset asset)
		{
				var fileName = Path.GetFileName(uploadResponse.FilePath);
				var extension = FileExtension.FromFileName(fileName, asset.TypeRef);

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
