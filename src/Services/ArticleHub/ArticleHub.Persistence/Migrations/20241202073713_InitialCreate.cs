using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "journal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journal", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    firstname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    lastname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    doi = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    stage = table.Column<string>(type: "text", nullable: false),
                    submittedbyid = table.Column<int>(type: "integer", nullable: false),
                    submittedon = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    acceptedon = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    publishedon = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    journalid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_journal_journalid",
                        column: x => x.journalid,
                        principalTable: "journal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_article_person_submittedbyid",
                        column: x => x.submittedbyid,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "articlecontributor",
                columns: table => new
                {
                    role = table.Column<string>(type: "text", nullable: false, defaultValue: "AUT"),
                    articleid = table.Column<int>(type: "integer", nullable: false),
                    personid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articlecontributor", x => new { x.articleid, x.personid, x.role });
                    table.ForeignKey(
                        name: "fk_articlecontributor_article_articleid",
                        column: x => x.articleid,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_articlecontributor_person_personid",
                        column: x => x.personid,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_journalid",
                table: "article",
                column: "journalid");

            migrationBuilder.CreateIndex(
                name: "IX_article_submittedbyid",
                table: "article",
                column: "submittedbyid");

            migrationBuilder.CreateIndex(
                name: "IX_article_title",
                table: "article",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "IX_articlecontributor_personid",
                table: "articlecontributor",
                column: "personid");

            migrationBuilder.CreateIndex(
                name: "IX_person_userid",
                table: "person",
                column: "userid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articlecontributor");

            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "journal");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
