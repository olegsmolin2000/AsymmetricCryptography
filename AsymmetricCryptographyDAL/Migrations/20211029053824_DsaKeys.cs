using Microsoft.EntityFrameworkCore.Migrations;

namespace AsymmetricCryptographyDAL.Migrations
{
    public partial class DsaKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DsaDomainParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlgorithmName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BinarySize = table.Column<int>(type: "int", nullable: false),
                    Q = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    G = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsaDomainParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DsaPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    DomainParameterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsaPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DsaPrivateKeys_DsaDomainParameters_DomainParameterId",
                        column: x => x.DomainParameterId,
                        principalTable: "DsaDomainParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DsaPrivateKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DsaPublicKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterId = table.Column<int>(type: "int", nullable: false),
                    DomainParameterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsaPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DsaPublicKeys_DsaDomainParameters_DomainParameterId",
                        column: x => x.DomainParameterId,
                        principalTable: "DsaDomainParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DsaPublicKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DsaPrivateKeys_DomainParameterId",
                table: "DsaPrivateKeys",
                column: "DomainParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_DsaPublicKeys_DomainParameterId",
                table: "DsaPublicKeys",
                column: "DomainParameterId");
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
