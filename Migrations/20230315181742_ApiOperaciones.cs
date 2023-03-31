using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    public partial class ApiOperaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mercados",
                columns: table => new
                {
                    IdMercado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameMarket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceMarket = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercados", x => x.IdMercado);
                });

            migrationBuilder.CreateTable(
                name: "Sentimientos",
                columns: table => new
                {
                    IdSentimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSentimiento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentimientos", x => x.IdSentimiento);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    IdOperation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TpOperationString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMercado = table.Column<int>(type: "int", nullable: false),
                    IdSentimiento = table.Column<int>(type: "int", nullable: false),
                    Stop = table.Column<int>(type: "int", nullable: false),
                    PrecioEntrada = table.Column<double>(type: "float", nullable: false),
                    PrecioSalida = table.Column<double>(type: "float", nullable: false),
                    Puntos = table.Column<int>(type: "int", nullable: false),
                    DineroOperacion = table.Column<double>(type: "float", nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MercadoFkIdMercado = table.Column<int>(type: "int", nullable: true),
                    SentimientoFkIdSentimiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.IdOperation);
                    table.ForeignKey(
                        name: "FK_Operations_Mercados_MercadoFkIdMercado",
                        column: x => x.MercadoFkIdMercado,
                        principalTable: "Mercados",
                        principalColumn: "IdMercado");
                    table.ForeignKey(
                        name: "FK_Operations_Sentimientos_SentimientoFkIdSentimiento",
                        column: x => x.SentimientoFkIdSentimiento,
                        principalTable: "Sentimientos",
                        principalColumn: "IdSentimiento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MercadoFkIdMercado",
                table: "Operations",
                column: "MercadoFkIdMercado");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_SentimientoFkIdSentimiento",
                table: "Operations",
                column: "SentimientoFkIdSentimiento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Mercados");

            migrationBuilder.DropTable(
                name: "Sentimientos");
        }
    }
}
