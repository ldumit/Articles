using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Production.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultCategoryId = table.Column<int>(type: "int", nullable: false),
                    AllowedFileExtentions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultFileExtension = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValue: "pdf"),
                    MaxNumber = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.Id);
                    table.UniqueConstraint("AK_AssetType_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.UniqueConstraint("AK_Stage_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Doi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SubmitedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmitedById = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    VolumeId = table.Column<int>(type: "int", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedById = table.Column<int>(type: "int", nullable: true),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: false),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 9, 13, 7, 44, 42, 500, DateTimeKind.Utc).AddTicks(5186))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Stage_Stage",
                        column: x => x.Stage,
                        principalTable: "Stage",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCurrentStage",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCurrentStage", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_ArticleCurrentStage_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleCurrentStage_Stage_Stage",
                        column: x => x.Stage,
                        principalTable: "Stage",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AssetNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    LatestFileId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: false),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 9, 13, 7, 44, 42, 505, DateTimeKind.Utc).AddTicks(6506))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_AssetType_TypeCode",
                        column: x => x.TypeCode,
                        principalTable: "AssetType",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PersonType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CompanyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StageHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageHistory_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StageHistory_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Full file name, with extension"),
                    FileServerId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false, comment: "Size of the file in kilobytes"),
                    Version = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "Final name of the file after renaming"),
                    Extension = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    LatestActionId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: false),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 9, 13, 7, 44, 42, 508, DateTimeKind.Utc).AddTicks(9969))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleActor",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "AUT")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleActor", x => new { x.ArticleId, x.PersonId, x.Role });
                    table.ForeignKey(
                        name: "FK_ArticleActor_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleActor_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DefaultTypesetterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journal_Person_DefaultTypesetterId",
                        column: x => x.DefaultTypesetterId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetLatestFile",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLatestFile", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_AssetLatestFile_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLatestFile_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAction_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileLatestAction",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileLatestAction", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileLatestAction_FileAction_ActionId",
                        column: x => x.ActionId,
                        principalTable: "FileAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileLatestAction_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssetType",
                columns: new[] { "Id", "AllowedFileExtentions", "Code", "DefaultCategoryId", "DefaultFileExtension", "Name" },
                values: new object[,]
                {
                    { 1, "[\"pdf\"]", "Manuscript", 1, "pdf", "Manuscript" },
                    { 2, "[\"pdf\"]", "ReviewReport", 3, "pdf", "Reviewer Report" },
                    { 3, "[\"pdf\"]", "DraftPdf", 3, "pdf", "Draft PDF" },
                    { 4, "[\"pdf\"]", "FinalPdf", 3, "pdf", "Final PDF" },
                    { 5, "[\"zip\"]", "FinalHtml", 3, "zip", "Final HTML Zip" },
                    { 6, "[\"epub\"]", "FinalEpub", 3, "epub", "Final Epub" }
                });

            migrationBuilder.InsertData(
                table: "AssetType",
                columns: new[] { "Id", "AllowedFileExtentions", "Code", "DefaultCategoryId", "DefaultFileExtension", "MaxNumber", "Name" },
                values: new object[,]
                {
                    { 7, "[\"jpg\",\"png\",\"tif\",\"tiff\",\"eps\"]", "Figure", 2, "tif", (byte)12, "HTML Figure" },
                    { 8, "[\"csv\",\"xls\"]", "DataSheet", 2, "csv", (byte)12, "Data Sheet" },
                    { 9, "[]", "SupplementaryFile", 2, "pdf", (byte)12, "Supplementary File" }
                });

            migrationBuilder.InsertData(
                table: "Stage",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { 100, "Submitted", "Our editorial specialist is checking your article. We will contact you if we need any further files or information.", "Article submitted" },
                    { 200, "InReview", "Your article has been checked and article. Our editorial specialists will start soon revieing it.", "Article approved" },
                    { 201, "Accepted", "Your article has been reviewed and accepted. The production of the article will start soon.", "Article accepted" },
                    { 300, "InProduction", "The typesetter is preparing your Author’s Proof. We will contact you if we need any further files or information.", "Typesetter assigned" },
                    { 301, "AuthorsProof", "The Author's Proof is available for you to check and provide corrections. This status is also displayed if we are preparing a further Author's Proof at your request.", "Author's proof approved" },
                    { 302, "FinalProduction", "The typesetter is preparing the final version of your article for publication. We will contact you if we need to check anything further before publication.", "Publisher's proof uploaded" },
                    { 303, "PublisherProof", "Your Production Specialist is applying quality checks to ensure your article is ready for publication.", "Article scheduled for publication" },
                    { 304, "PublicationScheduled", "Your Production Specialist has completed their quality checks. Your article is now scheduled for publication on our website and will appear online within the next few working days.", "Article scheduled for publication" },
                    { 305, "Published", "Your article has been published and sent to all relevant repositories, and the publication process is now complete. Please note that repositories have different processing times and your article may not be available yet.", "Article published" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_JournalId",
                table: "Article",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_PublishedById",
                table: "Article",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Stage",
                table: "Article",
                column: "Stage");

            migrationBuilder.CreateIndex(
                name: "IX_Article_SubmitedById",
                table: "Article",
                column: "SubmitedById");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title",
                table: "Article",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleActor_PersonId",
                table: "ArticleActor",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleCurrentStage_Stage",
                table: "ArticleCurrentStage",
                column: "Stage");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ArticleId",
                table: "Asset",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_TypeCode",
                table: "Asset",
                column: "TypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLatestFile_FileId",
                table: "AssetLatestFile",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_Code",
                table: "AssetType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArticleId_TypeId",
                table: "Comment",
                columns: new[] { "ArticleId", "TypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_AssetId",
                table: "File",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAction_FileId",
                table: "FileAction",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileLatestAction_ActionId",
                table: "FileLatestAction",
                column: "ActionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journal_DefaultTypesetterId",
                table: "Journal",
                column: "DefaultTypesetterId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ArticleId",
                table: "Person",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_Code",
                table: "Stage",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageHistory_ArticleId",
                table: "StageHistory",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_StageHistory_StageId",
                table: "StageHistory",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Journal_JournalId",
                table: "Article",
                column: "JournalId",
                principalTable: "Journal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Person_PublishedById",
                table: "Article",
                column: "PublishedById",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Person_SubmitedById",
                table: "Article",
                column: "SubmitedById",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Journal_JournalId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Person_PublishedById",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Person_SubmitedById",
                table: "Article");

            migrationBuilder.DropTable(
                name: "ArticleActor");

            migrationBuilder.DropTable(
                name: "ArticleCurrentStage");

            migrationBuilder.DropTable(
                name: "AssetLatestFile");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "FileLatestAction");

            migrationBuilder.DropTable(
                name: "StageHistory");

            migrationBuilder.DropTable(
                name: "FileAction");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Stage");
        }
    }
}
