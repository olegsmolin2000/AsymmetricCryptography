using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsymmetricCryptography.EFCore.Migrations
{
    public partial class DsaEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DsaDomainParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Q = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    G = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsaDomainParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DsaDomainParameters_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DsaPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomainParameterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsaPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterID",
                        column: x => x.DomainParameterID,
                        principalTable: "DsaDomainParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DsaPrivateKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DsaPublicKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomainParameterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsaPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterID",
                        column: x => x.DomainParameterID,
                        principalTable: "DsaDomainParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DsaPublicKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DsaPrivateKeys_DomainParameterID",
                table: "DsaPrivateKeys",
                column: "DomainParameterID");

            migrationBuilder.CreateIndex(
                name: "IX_DsaPublicKeys_DomainParameterID",
                table: "DsaPublicKeys",
                column: "DomainParameterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DsaPrivateKeys");

            migrationBuilder.DropTable(
                name: "DsaPublicKeys");

            migrationBuilder.DropTable(
                name: "DsaDomainParameters");
        }
    }
}
