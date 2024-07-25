using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Production.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultCategoryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                    table.UniqueConstraint("AK_AssetTypes_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Typesetters",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typesetters", x => x.UserId);
                    table.UniqueConstraint("AK_Typesetters_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Typesetters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DefaultTypesetterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_Typesetters_DefaultTypesetterId",
                        column: x => x.DefaultTypesetterId,
                        principalTable: "Typesetters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Doi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubmitedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmitedById = table.Column<int>(type: "int", nullable: false),
                    CurrentStageId = table.Column<int>(type: "int", nullable: false),
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    VolumeId = table.Column<int>(type: "int", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedById = table.Column<int>(type: "int", nullable: true),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypesetterId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: false),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 7, 26, 15, 45, 25, 925, DateTimeKind.Utc).AddTicks(6931))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_Typesetters_TypesetterId",
                        column: x => x.TypesetterId,
                        principalTable: "Typesetters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_Users_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_Users_SubmitedById",
                        column: x => x.SubmitedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCurrentStage",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCurrentStage", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_ArticleCurrentStage_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleCurrentStage_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssetNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LatestFileId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: false),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 7, 26, 15, 45, 25, 932, DateTimeKind.Utc).AddTicks(5356))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_AssetTypes_TypeCode",
                        column: x => x.TypeCode,
                        principalTable: "AssetTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "AUT"),
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StageHistories",
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
                    table.PrimaryKey("PK_StageHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageHistories_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StageHistories_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Full file name, with extension"),
                    FileServerId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false, comment: "Size of the file in kilobytes"),
                    Version = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Final name of the file after renaming"),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    LatestActionId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: false),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2024, 7, 26, 15, 45, 25, 937, DateTimeKind.Utc).AddTicks(8372))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_AssetLatestFile_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
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
                name: "FileActions",
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
                    table.PrimaryKey("PK_FileActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileActions_File_FileId",
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
                        name: "FK_FileLatestAction_FileActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "FileActions",
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
                table: "AssetTypes",
                columns: new[] { "Id", "Code", "DefaultCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Manuscript", 1, "Manuscript" },
                    { 2, "ReviewReport", 3, "Reviewer Report" },
                    { 3, "AuthorsProof", 3, "Author's Proof" },
                    { 4, "PublishersProof", 3, "Publisher's Proof" },
                    { 5, "HTML", 3, "HTML" },
                    { 6, "XML", 3, "XML Zip" },
                    { 7, "Figure", 2, "HTML Figure" },
                    { 8, "SupplementaryFile", 2, "Supplementary File" }
                });

            migrationBuilder.InsertData(
                table: "Stages",
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
                name: "IX_ArticleCurrentStage_StageId",
                table: "ArticleCurrentStage",
                column: "StageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_JournalId",
                table: "Articles",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublishedById",
                table: "Articles",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SubmitedById",
                table: "Articles",
                column: "SubmitedById");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Title",
                table: "Articles",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TypesetterId",
                table: "Articles",
                column: "TypesetterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLatestFile_FileId",
                table: "AssetLatestFile",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ArticleId",
                table: "Assets",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TypeCode",
                table: "Assets",
                column: "TypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_Code",
                table: "AssetTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_ArticleId",
                table: "Authors",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_FirstName_LastName",
                table: "Authors",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId_TypeId",
                table: "Comments",
                columns: new[] { "ArticleId", "TypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_AssetId",
                table: "File",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_FileActions_FileId",
                table: "FileActions",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileLatestAction_ActionId",
                table: "FileLatestAction",
                column: "ActionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_DefaultTypesetterId",
                table: "Journals",
                column: "DefaultTypesetterId");

            migrationBuilder.CreateIndex(
                name: "IX_StageHistories_ArticleId",
                table: "StageHistories",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_StageHistories_StageId",
                table: "StageHistories",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_Code",
                table: "Stages",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleCurrentStage");

            migrationBuilder.DropTable(
                name: "AssetLatestFile");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FileLatestAction");

            migrationBuilder.DropTable(
                name: "StageHistories");

            migrationBuilder.DropTable(
                name: "FileActions");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AssetTypes");

            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropTable(
                name: "Typesetters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
