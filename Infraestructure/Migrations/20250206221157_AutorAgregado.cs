using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AutorAgregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "moderador_name",
                table: "AspNetUsers",
                newName: "staff_name");

            migrationBuilder.AddColumn<string>(
                name: "autor_role",
                table: "hilos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "autor_username",
                table: "hilos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "autor_role",
                table: "comentarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "autor_username",
                table: "comentarios",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "autor_role",
                table: "hilos");

            migrationBuilder.DropColumn(
                name: "autor_username",
                table: "hilos");

            migrationBuilder.DropColumn(
                name: "autor_role",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "autor_username",
                table: "comentarios");

            migrationBuilder.RenameColumn(
                name: "staff_name",
                table: "AspNetUsers",
                newName: "moderador_name");
        }
    }
}
