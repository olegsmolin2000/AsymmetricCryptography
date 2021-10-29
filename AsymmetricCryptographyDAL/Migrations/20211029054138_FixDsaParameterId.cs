using Microsoft.EntityFrameworkCore.Migrations;

namespace AsymmetricCryptographyDAL.Migrations
{
    public partial class FixDsaParameterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPrivateKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPublicKeys");

            migrationBuilder.DropColumn(
                name: "ParameterId",
                table: "DsaPublicKeys");

            migrationBuilder.DropColumn(
                name: "ParameterId",
                table: "DsaPrivateKeys");

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

            migrationBuilder.AddColumn<int>(
                name: "ParameterId",
                table: "DsaPublicKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DomainParameterId",
                table: "DsaPrivateKeys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParameterId",
                table: "DsaPrivateKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPrivateKeys",
                column: "DomainParameterId",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                table: "DsaPublicKeys",
                column: "DomainParameterId",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
