using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ArticleTimeline.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ArticleTimeline");

            migrationBuilder.CreateTable(
                name: "TimelineTemplate",
                schema: "ArticleTimeline",
                columns: table => new
                {
                    SourceType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SourceId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TitleTemplate = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DescriptionTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimelineTemplate", x => new { x.SourceType, x.SourceId });
                });

            migrationBuilder.CreateTable(
                name: "TimelineVisibility",
                schema: "ArticleTimeline",
                columns: table => new
                {
                    SourceType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SourceId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RoleType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimelineVisibility", x => new { x.SourceType, x.SourceId, x.RoleType });
                });

            migrationBuilder.CreateTable(
                name: "Timelines",
                schema: "ArticleTimeline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CurrentStage = table.Column<int>(type: "int", nullable: false),
                    PreviousStage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SourceId = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<int>(type: "int", nullable: true),
                    LasModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timelines_TimelineTemplate_SourceType_SourceId",
                        columns: x => new { x.SourceType, x.SourceId },
                        principalSchema: "ArticleTimeline",
                        principalTable: "TimelineTemplate",
                        principalColumns: new[] { "SourceType", "SourceId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "ArticleTimeline",
                table: "TimelineTemplate",
                columns: new[] { "SourceId", "SourceType", "DescriptionTemplate", "TitleTemplate" },
                values: new object[,]
                {
                    { "Accepted->InProduction", "StageTransition", "<<RoleUser>> has started production.", "<<PreviousStage>> stage completed" },
                    { "DraftProduction->FinalProduction", "StageTransition", "<<RoleUser>> has aproved production.", "<<PreviousStage>> stage completed" },
                    { "FinalProduction->Published", "StageTransition", "<<RoleUser>> has published the article.", "<<PreviousStage>> stage completed" },
                    { "InProduction->DraftProduction", "StageTransition", "<<RoleUser>> has uploaded the Draft Pdf.", "<<PreviousStage>> stage completed" }
                });

            migrationBuilder.InsertData(
                schema: "ArticleTimeline",
                table: "TimelineVisibility",
                columns: new[] { "RoleType", "SourceId", "SourceType" },
                values: new object[,]
                {
                    { "POF", "Accepted->InProduction", "StageTransition" },
                    { "TSOF", "Accepted->InProduction", "StageTransition" },
                    { "POF", "DraftProduction->FinalProduction", "StageTransition" },
                    { "TSOF", "DraftProduction->FinalProduction", "StageTransition" },
                    { "CORAUT", "FinalProduction->Published", "StageTransition" },
                    { "POF", "FinalProduction->Published", "StageTransition" },
                    { "TSOF", "FinalProduction->Published", "StageTransition" },
                    { "CORAUT", "InProduction->DraftProduction", "StageTransition" },
                    { "POF", "InProduction->DraftProduction", "StageTransition" },
                    { "TSOF", "InProduction->DraftProduction", "StageTransition" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_SourceType_SourceId",
                schema: "ArticleTimeline",
                table: "Timelines",
                columns: new[] { "SourceType", "SourceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timelines",
                schema: "ArticleTimeline");

            migrationBuilder.DropTable(
                name: "TimelineVisibility",
                schema: "ArticleTimeline");

            migrationBuilder.DropTable(
                name: "TimelineTemplate",
                schema: "ArticleTimeline");
        }
    }
}
