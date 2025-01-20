using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombreDeTablaHiloInteraccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hilo_interaccion");

            migrationBuilder.CreateTable(
                name: "hilo_interacciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seguido = table.Column<bool>(type: "boolean", nullable: false),
                    favorito = table.Column<bool>(type: "boolean", nullable: false),
                    oculto = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hilo_interacciones", x => x.id);
                    table.ForeignKey(
                        name: "fk_hilo_interacciones_asp_net_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_hilo_interacciones_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_hilo_interacciones_hilo_id",
                table: "hilo_interacciones",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_hilo_interacciones_usuario_id",
                table: "hilo_interacciones",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hilo_interacciones");

            migrationBuilder.CreateTable(
                name: "hilo_interaccion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    favorito = table.Column<bool>(type: "boolean", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    oculto = table.Column<bool>(type: "boolean", nullable: false),
                    seguido = table.Column<bool>(type: "boolean", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hilo_interaccion", x => x.id);
                    table.ForeignKey(
                        name: "fk_hilo_interaccion_asp_net_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_hilo_interaccion_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_hilo_interaccion_hilo_id",
                table: "hilo_interaccion",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_hilo_interaccion_usuario_id",
                table: "hilo_interaccion",
                column: "usuario_id");
        }
    }
}
