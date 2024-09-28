using Articles.Entitities;
using Production.Domain.Enums;
using Production.Domain.ValueObjects;


namespace Production.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetType : EnumEntity<Enums.AssetType>
{
    public AssetCategory DefaultCategoryId { get; init; }

    //todo - when uploading validate the File extension against this list
    //public IReadOnlyList<string> AllowedFileExtentions { get; init; } = ImmutableList<string>.Empty;
    public AllowedFileExtensions AllowedFileExtensions { get; init; } = null!;
		
  //  private AllowedFileExtensions _allowedFileExtensions;
		//public IReadOnlyList<string> AllowedFileExtensions => _allowedFileExtensions.Extensions;

		public string DefaultFileExtension { get; init; } = default!;
    public byte MaxNumber { get; init; }

    //public required string Description { get; set; }
}
