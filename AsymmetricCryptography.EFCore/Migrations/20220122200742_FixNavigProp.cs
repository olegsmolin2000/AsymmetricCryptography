using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    public partial class FixNavigProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPrivateKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPublicKeys");

            migrationBuilder.AlterColumn<int>(
                name: "DomainParameterId",
                table: "DsaPublicKeys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DomainParameterId",
                table: "DsaPrivateKeys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPrivateKeys",
                column: "DomainParameterId",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPublicKeys",
                column: "DomainParameterId",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPrivateKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPublicKeys");

            migrationBuilder.AlterColumn<int>(
                name: "DomainParameterId",
                table: "DsaPublicKeys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DomainParameterId",
                table: "DsaPrivateKeys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPrivateKeys",
                column: "DomainParameterId",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPublicKeys",
                column: "DomainParameterId",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id");
        }
    }
}
