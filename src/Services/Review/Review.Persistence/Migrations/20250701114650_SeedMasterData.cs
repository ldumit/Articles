using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedMasterData : Migration
    {
				/// <inheritdoc />
				protected override void Up(MigrationBuilder migrationBuilder)
				{
						migrationBuilder.Sql(@"
            INSERT INTO [AssetTypeDefinition] 
            (id, name, description, defaultCategoryId, maxAssetCount, allowedFileExtensions, defaultFileExtension, MaxFileSizeInMB)
            VALUES
            (1, 'Manuscript', 'Manuscript', 1, 0, '[""pdf""]', 'pdf', 50),
            (2, 'ReviewReport', 'Reviewer Report', 3, 0, '[""pdf""]', 'pdf', 50),
            (3, 'DraftPdf', 'Draft PDF', 3, 0, '[""pdf""]', 'pdf', 50),
            (4, 'FinalPdf', 'Final PDF', 3, 0, '[""pdf""]', 'pdf', 50),
            (5, 'FinalHtml', 'Final HTML Zip', 3, 0, '[""zip""]', 'zip', 100),
            (6, 'FinalEpub', 'Final Epub', 3, 0, '[""epub""]', 'epub', 5),
            (7, 'Figure', 'HTML Figure', 2, 12, '[""jpg"",""png"",""tif"",""tiff"",""eps""]', 'tif', 10),
            (8, 'DataSheet', 'Data Sheet', 2, 12, '[""csv"",""xls""]', 'csv', 1),
            (9, 'SupplementaryFile', 'Supplementary File', 2, 12, '[]', 'pdf', 10);
        ");
				}

				/// <inheritdoc />
				protected override void Down(MigrationBuilder migrationBuilder)
				{
						migrationBuilder.Sql("DELETE FROM [AssetTypeDefinition]");
				}
		}
}
