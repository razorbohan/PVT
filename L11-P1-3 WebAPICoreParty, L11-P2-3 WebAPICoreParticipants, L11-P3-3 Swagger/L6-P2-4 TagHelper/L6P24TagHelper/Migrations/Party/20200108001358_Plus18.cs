using Microsoft.EntityFrameworkCore.Migrations;

namespace L6P24TagHelper.Migrations.Party
{
    public partial class Plus18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlus18",
                table: "Parties",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlus18",
                table: "Parties");
        }
    }
}
