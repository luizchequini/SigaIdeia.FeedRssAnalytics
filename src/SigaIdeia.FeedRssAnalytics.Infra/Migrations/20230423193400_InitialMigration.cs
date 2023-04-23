using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigaIdeia.FeedRssAnalytics.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleMatrix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<string>(type: "varchar(100)", nullable: false),
                    Author = table.Column<string>(type: "varchar(150)", nullable: false),
                    Link = table.Column<string>(type: "varchar(max)", nullable: false),
                    Title = table.Column<string>(type: "varchar(300)", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    Category = table.Column<string>(type: "varchar(150)", nullable: false),
                    Views = table.Column<string>(type: "varchar(max)", nullable: true),
                    ViewsCount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    PubDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleMatrix", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleMatrix");
        }
    }
}
