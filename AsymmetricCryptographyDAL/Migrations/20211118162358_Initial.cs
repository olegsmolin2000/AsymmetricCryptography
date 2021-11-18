using Microsoft.EntityFrameworkCore.Migrations;

namespace AsymmetricCryptographyDAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlgorithmName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BinarySize = table.Column<int>(type: "int", nullable: false),
                    NumberGenerator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimalityVerificator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashAlgorithm = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "RsaPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Modulus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivateExponent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RsaPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RsaPrivateKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RsaPublicKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Modulus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicExponent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RsaPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RsaPublicKeys_Keys_Id",
                        column: x => x.Id,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DsaPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ElGamalPrivateKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyParameterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalPrivateKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalPrivateKeys_ElGamalKeyParameters_KeyParameterId",
                        column: x => x.KeyParameterId,
                        principalTable: "ElGamalKeyParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    KeyParameterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGamalPublicKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElGamalPublicKeys_ElGamalKeyParameters_KeyParameterId",
                        column: x => x.KeyParameterId,
                        principalTable: "ElGamalKeyParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElGamalPublicKeys_Keys_Id",
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

            migrationBuilder.CreateIndex(
                name: "IX_ElGamalPrivateKeys_KeyParameterId",
                table: "ElGamalPrivateKeys",
                column: "KeyParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGamalPublicKeys_KeyParameterId",
                table: "ElGamalPublicKeys",
                column: "KeyParameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DsaPrivateKeys");

            migrationBuilder.DropTable(
                name: "DsaPublicKeys");

            migrationBuilder.DropTable(
                name: "ElGamalPrivateKeys");

            migrationBuilder.DropTable(
                name: "ElGamalPublicKeys");

            migrationBuilder.DropTable(
                name: "RsaPrivateKeys");

            migrationBuilder.DropTable(
                name: "RsaPublicKeys");

            migrationBuilder.DropTable(
                name: "DsaDomainParameters");

            migrationBuilder.DropTable(
                name: "ElGamalKeyParameters");

            migrationBuilder.DropTable(
                name: "Keys");
        }
    }
}
