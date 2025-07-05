using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Submission.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleStageTransition",
                columns: table => new
                {
                    CurrentStage = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DestinationStage = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleStageTransition", x => new { x.CurrentStage, x.ActionType, x.DestinationStage });
                });

            migrationBuilder.CreateTable(
                name: "AssetTypeDefinition",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DefaultCategoryId = table.Column<int>(type: "int", nullable: false),
                    DefaultFileExtension = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValue: "pdf"),
                    MaxAssetCount = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    MaxFileSizeInMB = table.Column<byte>(type: "tinyint", nullable: false),
                    AllowedFileExtensions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypeDefinition", x => x.Id);
                    table.UniqueConstraint("AK_AssetTypeDefinition_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
										Honorific = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Affiliation = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false, comment: "Institution or organization they are associated with when they conduct their research."),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TypeDiscriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true, comment: "The author's highest academic qualification (e.g., PhD in Mathematics, MSc in Chemistry)."),
                    Discipline = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "The author's main field of study or research (e.g., Biology, Computer Science)."),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.UniqueConstraint("AK_Stage_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedById = table.Column<int>(type: "int", nullable: true),
                    Stage = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_Person_SubmittedById",
                        column: x => x.SubmittedById,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_Stage_Stage",
                        column: x => x.Stage,
                        principalTable: "Stage",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleAction_Article_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleActor",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "AUT"),
                    TypeDiscriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ContributionAreas = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Type = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    File_FileServerId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    File_OriginalName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "Original full file name, with extension"),
                    File_Size = table.Column<long>(type: "bigint", nullable: false, comment: "Size of the file in kilobytes"),
                    File_Extension = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    File_Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, comment: "Final name of the file after renaming"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        name: "FK_Asset_AssetTypeDefinition_Type",
                        column: x => x.Type,
                        principalTable: "AssetTypeDefinition",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "ArticleStageTransition",
                columns: new[] { "ActionType", "CurrentStage", "DestinationStage" },
                values: new object[,]
                {
                    { "UploadAsset", "Created", "ManuscriptUploaded" },
                    { "SubmitDraft", "ManuscriptUploaded", "Submitted" },
                    { "UploadAsset", "ManuscriptUploaded", "ManuscriptUploaded" },
                    { "CreateArticle", "None", "Created" },
                    { "ApproveDraft", "Submitted", "InitialApproved" },
                    { "RejectDraft", "Submitted", "InitialRejected" }
                });

            migrationBuilder.InsertData(
                table: "Stage",
                columns: new[] { "Id", "Description", "Info", "Name" },
                values: new object[,]
                {
                    { 101, "The Author created the article", "The article was created. Please upload the Manuscript and the Supplimentarry materials. Associate the authors with the article.", "Created" },
                    { 102, "The Manuscript was submitted by the author", "Our editorial specialist is checking your article. We will contact you if we need any further files or information.", "ManuscriptUploaded" },
                    { 103, "Author uploaded the Manuscript", "The manuscript was uploaded, you can now submit the article.", "Submitted" },
                    { 104, "Article was rejected by the editorial specialist", "The manuscript does not reach the required quality standard of this journal.", "InitialRejected" },
                    { 105, "Article approved", "Your article has been checked. Our editorial specialists will start soon revieing it.", "InitialApproved" },
                    { 201, "Article approved", "Our editorial specialist is reviewing your article.", "InReview" },
                    { 204, "Article accepted", "Your article has been reviewed and accepted. The production of the article will start soon.", "Accepted" },
                    { 300, "Typesetter assigned", "The typesetter is preparing your Author’s Proof. We will contact you if we need any further files or information.", "InProduction" },
                    { 301, "Author's proof approved", "The Author's Proof is available for you to check and provide corrections. This status is also displayed if we are preparing a further Author's Proof at your request.", "DraftProduction" },
                    { 302, "Publisher's proof uploaded", "The typesetter is preparing the final version of your article for publication. We will contact you if we need to check anything further before publication.", "FinalProduction" },
                    { 304, "Article scheduled for publication", "Your Production Specialist has completed their quality checks. Your article is now scheduled for publication on our website and will appear online within the next few working days.", "PublicationScheduled" },
                    { 305, "Article published", "Your article has been published and sent to all relevant repositories, and the publication process is now complete. Please note that repositories have different processing times and your article may not be available yet.", "Published" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_JournalId",
                table: "Article",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Stage",
                table: "Article",
                column: "Stage");

            migrationBuilder.CreateIndex(
                name: "IX_Article_SubmittedById",
                table: "Article",
                column: "SubmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title",
                table: "Article",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAction_EntityId",
                table: "ArticleAction",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleActor_PersonId",
                table: "ArticleActor",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ArticleId",
                table: "Asset",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Type",
                table: "Asset",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypeDefinition_Name",
                table: "AssetTypeDefinition",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_Name",
                table: "Stage",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageHistory_ArticleId",
                table: "StageHistory",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_StageHistory_StageId",
                table: "StageHistory",
                column: "StageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleAction");

            migrationBuilder.DropTable(
                name: "ArticleActor");

            migrationBuilder.DropTable(
                name: "ArticleStageTransition");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "StageHistory");

            migrationBuilder.DropTable(
                name: "AssetTypeDefinition");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Stage");
        }
    }
}
