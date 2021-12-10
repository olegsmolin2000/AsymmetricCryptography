using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    public partial class TestTptAndEnumConversions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KeyType",
                table: "Keys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AlgorithmName",
                table: "Keys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ElGamalPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    P = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    G = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalPrivateKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ElGamalPublicKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    P = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    G = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Y = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalPublicKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RsaPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PrivateExponent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modulus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RsaPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RsaPrivateKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RsaPublicKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PublicExponent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modulus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RsaPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RsaPublicKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElGamalPrivateKeys");

            migrationBuilder.DropTable(
                name: "ElGamalPublicKeys");

            migrationBuilder.DropTable(
                name: "RsaPrivateKeys");

            migrationBuilder.DropTable(
                name: "RsaPublicKeys");

            migrationBuilder.AlterColumn<int>(
                name: "KeyType",
                table: "Keys",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AlgorithmName",
                table: "Keys",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
