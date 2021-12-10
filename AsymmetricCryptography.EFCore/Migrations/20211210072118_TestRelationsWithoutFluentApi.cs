using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    public partial class TestRelationsWithoutFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterID",
                table: "DsaPrivateKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterID",
                table: "DsaPublicKeys");

            migrationBuilder.RenameColumn(
                name: "DomainParameterID",
                table: "DsaPublicKeys",
                newName: "DomainParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_DsaPublicKeys_DomainParameterID",
                table: "DsaPublicKeys",
                newName: "IX_DsaPublicKeys_DomainParameterId");

            migrationBuilder.RenameColumn(
                name: "DomainParameterID",
                table: "DsaPrivateKeys",
                newName: "DomainParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_DsaPrivateKeys_DomainParameterID",
                table: "DsaPrivateKeys",
                newName: "IX_DsaPrivateKeys_DomainParameterId");

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

            migrationBuilder.RenameColumn(
                name: "DomainParameterId",
                table: "DsaPublicKeys",
                newName: "DomainParameterID");

            migrationBuilder.RenameIndex(
                name: "IX_DsaPublicKeys_DomainParameterId",
                table: "DsaPublicKeys",
                newName: "IX_DsaPublicKeys_DomainParameterID");

            migrationBuilder.RenameColumn(
                name: "DomainParameterId",
                table: "DsaPrivateKeys",
                newName: "DomainParameterID");

            migrationBuilder.RenameIndex(
                name: "IX_DsaPrivateKeys_DomainParameterId",
                table: "DsaPrivateKeys",
                newName: "IX_DsaPrivateKeys_DomainParameterID");

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterID",
                table: "DsaPrivateKeys",
                column: "DomainParameterID",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterID",
                table: "DsaPublicKeys",
                column: "DomainParameterID",
                principalTable: "DsaDomainParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
