using Articles.Entitities;
using Production.Domain.Enums;
using System.Collections.Immutable;


namespace Production.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetType : EnumEntity<Enums.AssetType>
{
    public AssetType()
    {

		}
    public AssetCategory DefaultCategoryId { get; set; }

    //todo - when uploading validate the File extension against this list
    public IReadOnlyList<string> AllowedFileExtentions { get; init; } = ImmutableList<string>.Empty;

		public string DefaultFileExtension { get; set; }
    public byte MaxNumber { get; set; } = 0;

    //public required string Description { get; set; }
}
