using Microsoft.EntityFrameworkCore.Migrations;

namespace AsymmetricCryptographyDAL.Migrations
{
    public partial class RefactorElGamalKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElGamalPrivateKeys_ElGamalKeyParameters_KeyParameterId",
                table: "ElGamalPrivateKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_ElGamalPublicKeys_ElGamalKeyParameters_KeyParameterId",
                table: "ElGamalPublicKeys");

            migrationBuilder.DropTable(
                name: "ElGamalKeyParameters");

            migrationBuilder.DropIndex(
                name: "IX_ElGamalPublicKeys_KeyParameterId",
                table: "ElGamalPublicKeys");

            migrationBuilder.DropIndex(
                name: "IX_ElGamalPrivateKeys_KeyParameterId",
                table: "ElGamalPrivateKeys");

            migrationBuilder.DropColumn(
                name: "KeyParameterId",
                table: "ElGamalPublicKeys");

            migrationBuilder.DropColumn(
                name: "KeyParameterId",
                table: "ElGamalPrivateKeys");

            migrationBuilder.AddColumn<string>(
                name: "G",
                table: "ElGamalPublicKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "P",
                table: "ElGamalPublicKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "G",
                table: "ElGamalPrivateKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "P",
                table: "ElGamalPrivateKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "G",
                table: "ElGamalPublicKeys");

            migrationBuilder.DropColumn(
                name: "P",
                table: "ElGamalPublicKeys");

            migrationBuilder.DropColumn(
                name: "G",
                table: "ElGamalPrivateKeys");

            migrationBuilder.DropColumn(
                name: "P",
                table: "ElGamalPrivateKeys");

            migrationBuilder.AddColumn<int>(
                name: "KeyParameterId",
                table: "ElGamalPublicKeys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KeyParameterId",
                table: "ElGamalPrivateKeys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ElGamalKeyParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    G = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ElGamalPublicKeys_KeyParameterId",
                table: "ElGamalPublicKeys",
                column: "KeyParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGamalPrivateKeys_KeyParameterId",
                table: "ElGamalPrivateKeys",
                column: "KeyParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElGamalPrivateKeys_ElGamalKeyParameters_KeyParameterId",
                table: "ElGamalPrivateKeys",
                column: "KeyParameterId",
                principalTable: "ElGamalKeyParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ElGamalPublicKeys_ElGamalKeyParameters_KeyParameterId",
                table: "ElGamalPublicKeys",
                column: "KeyParameterId",
                principalTable: "ElGamalKeyParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
