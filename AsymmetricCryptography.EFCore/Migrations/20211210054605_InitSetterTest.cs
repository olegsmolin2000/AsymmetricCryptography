using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    public partial class InitSetterTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BinarySize",
                table: "Keys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BinarySize",
                table: "Keys");
        }
    }
}
