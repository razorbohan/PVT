using Microsoft.EntityFrameworkCore.Migrations;

namespace ITNews.Data.Migrations
{
    public partial class TagsNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_News_NewsId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_NewsId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "NewsTags",
                columns: table => new
                {
                    NewsId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTags", x => new { x.NewsId, x.TagId });
                    table.ForeignKey(
                        name: "FK_NewsTags_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsTags_TagId",
                table: "NewsTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsTags");

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_NewsId",
                table: "Tags",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_News_NewsId",
                table: "Tags",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
