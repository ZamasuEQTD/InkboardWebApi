using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class create_comentarios_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_comentarios_en_hilo;
                CREATE VIEW vw_comentarios_en_hilo AS
                SELECT
                    c.id,
                    c.texto,
                    c.tag,
                    c.color,
                    c.tag_unico,
                    c.autor_role,
                    c.autor_username AS autor,
                    c.autor_id,
                    c.hilo_id,
                    h.autor_id AS autor_hilo_id,
                    c.dados,
                    c.created_at,
                    c.status,
                    c.recibir_notificaciones,
                    media.url,
                    media.previsualizacion,
                    media.provider,
                    spoiler.spoiler
                FROM comentarios c
                JOIN hilos h ON h.id = c.hilo_id
                LEFT JOIN medias_spoileables spoiler ON c.media_id = spoiler.id
                LEFT JOIN medias media ON spoiler.hashed_media_id = media.id
            ");


            migrationBuilder.Sql(@"
                    DROP VIEW IF EXISTS vw_comentarios_destacados_en_hilo;
                    CREATE VIEW vw_comentarios_destacados_en_hilo AS
                    SELECT
                        c.*
                    FROM vw_comentarios_en_hilo c
                    JOIN comentario_destacado d ON c.id = d.comentario_id
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_comentarios_en_hilo");
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_comentarios_destacados_en_hilo");

        }
    }
}
