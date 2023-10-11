using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class probandoCitaError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Servicio_ServicioId",
                table: "Cita");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "Cita",
                newName: "IdServicio");

            migrationBuilder.RenameIndex(
                name: "IX_Cita_ServicioId",
                table: "Cita",
                newName: "IX_Cita_IdServicio");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Servicio_IdServicio",
                table: "Cita",
                column: "IdServicio",
                principalTable: "Servicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Servicio_IdServicio",
                table: "Cita");

            migrationBuilder.RenameColumn(
                name: "IdServicio",
                table: "Cita",
                newName: "ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_Cita_IdServicio",
                table: "Cita",
                newName: "IX_Cita_ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Servicio_ServicioId",
                table: "Cita",
                column: "ServicioId",
                principalTable: "Servicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
