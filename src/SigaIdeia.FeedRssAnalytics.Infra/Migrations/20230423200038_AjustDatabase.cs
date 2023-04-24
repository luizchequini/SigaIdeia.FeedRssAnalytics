using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigaIdeia.FeedRssAnalytics.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AjustDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleMatrix",
                table: "ArticleMatrix");

            migrationBuilder.RenameTable(
                name: "ArticleMatrix",
                newName: "ArticleMatrices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleMatrices",
                table: "ArticleMatrices",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleMatrices",
                table: "ArticleMatrices");

            migrationBuilder.RenameTable(
                name: "ArticleMatrices",
                newName: "ArticleMatrix");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleMatrix",
                table: "ArticleMatrix",
                column: "Id");
        }
    }
}
