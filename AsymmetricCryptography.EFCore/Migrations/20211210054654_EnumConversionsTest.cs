using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    public partial class EnumConversionsTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlgorithmName",
                table: "Keys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HashAlgorithm",
                table: "Keys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KeyType",
                table: "Keys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumberGenerator",
                table: "Keys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimalityVerificator",
                table: "Keys",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlgorithmName",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "HashAlgorithm",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "KeyType",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "NumberGenerator",
                table: "Keys");

            migrationBuilder.DropColumn(
                name: "PrimalityVerificator",
                table: "Keys");
        }
    }
}
