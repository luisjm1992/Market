using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    public partial class TablaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Cuentas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_UsuarioId",
                table: "Cuentas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_AspNetUsers_UsuarioId",
                table: "Cuentas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_AspNetUsers_UsuarioId",
                table: "Cuentas");

            migrationBuilder.DropIndex(
                name: "IX_Cuentas_UsuarioId",
                table: "Cuentas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Cuentas");
        }
    }
}
