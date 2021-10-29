using Microsoft.EntityFrameworkCore.Migrations;

namespace AsymmetricCryptographyDAL.Migrations
{
    public partial class ElGamalKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlgorithmName",
                table: "DsaDomainParameters");

            migrationBuilder.DropColumn(
                name: "BinarySize",
                table: "DsaDomainParameters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DsaDomainParameters");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DsaDomainParameters");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DsaDomainParameters",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "ElGamalKeyParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    P = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    G = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalKeyParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalKeyParameters_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElGamalPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyParameterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalPrivateKeys_ElGamalKeyParameters_KeyParameterId",
                        column: x => x.KeyParameterId,
                        principalTable: "ElGamalKeyParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElGamalPrivateKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElGamalPublicKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyParameterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalPublicKeys_ElGamalKeyParameters_KeyParameterId",
                        column: x => x.KeyParameterId,
                        principalTable: "ElGamalKeyParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElGamalPublicKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElGamalPrivateKeys_KeyParameterId",
                table: "ElGamalPrivateKeys",
                column: "KeyParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGamalPublicKeys_KeyParameterId",
                table: "ElGamalPublicKeys",
                column: "KeyParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DsaDomainParameters_Keys_Id",
                table: "DsaDomainParameters",
                column: "Id",
                principalTable: "Keys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DsaDomainParameters_Keys_Id",
                table: "DsaDomainParameters");

            migrationBuilder.DropTable(
                name: "ElGamalPrivateKeys");

            migrationBuilder.DropTable(
                name: "ElGamalPublicKeys");

            migrationBuilder.DropTable(
                name: "ElGamalKeyParameters");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DsaDomainParameters",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "AlgorithmName",
                table: "DsaDomainParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BinarySize",
                table: "DsaDomainParameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DsaDomainParameters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DsaDomainParameters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
