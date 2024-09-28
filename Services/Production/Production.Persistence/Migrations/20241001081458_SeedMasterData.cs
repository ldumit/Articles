using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Production.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedMasterData : Migration
    {
				/// <inheritdoc />
				protected override void Up(MigrationBuilder migrationBuilder)
				{
						migrationBuilder.Sql(@"
            INSERT INTO [AssetType] 
            (id, code, name, defaultCategoryId, maxNumber, allowedFileExtensions, defaultFileExtension)
            VALUES
            (1, 'Manuscript', 'Manuscript', 1, 0, '[""pdf""]', 'pdf'),
            (2, 'ReviewReport', 'Reviewer Report', 3, 0, '[""pdf""]', 'pdf'),
            (3, 'DraftPdf', 'Draft PDF', 3, 0, '[""pdf""]', 'pdf'),
            (4, 'FinalPdf', 'Final PDF', 3, 0, '[""pdf""]', 'pdf'),
            (5, 'FinalHtml', 'Final HTML Zip', 3, 0, '[""zip""]', 'zip'),
            (6, 'FinalEpub', 'Final Epub', 3, 0, '[""epub""]', 'epub'),
            (7, 'Figure', 'HTML Figure', 2, 12, '[""jpg"",""png"",""tif"",""tiff"",""eps""]', 'tif'),
            (8, 'DataSheet', 'Data Sheet', 2, 12, '[""csv"",""xls""]', 'csv'),
            (9, 'SupplementaryFile', 'Supplementary File', 2, 12, '[]', 'pdf');
        ");
				}

				protected override void Down(MigrationBuilder migrationBuilder)
				{
						// Optionally, implement the logic to remove the inserted rows in the Down method.
						migrationBuilder.Sql("DELETE FROM [AssetType] WHERE id IN (1, 2, 3, 4, 5, 6, 7, 8, 9);");
				}
		}
}
