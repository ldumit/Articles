using Production.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Domain.ValueObjects;

public record FileExtension
{
    private FileExtension(string file)
    {
        
    }
    public static  FileExtension FromFileName(string fileName, AssetType assetType)
    {
				var extension = Path.GetExtension(fileName);

        assetType.AllowedFileExtentions.Contains(extension);

        return new FileExtension(extension);
    }
}
