using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class nueva_view_comentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_comentarios_destacados_en_hilo;
                DROP VIEW IF EXISTS vw_comentarios_en_hilo;
            
                DROP VIEW IF EXISTS vw_comentarios_con_relaciones;
                CREATE VIEW vw_comentarios_con_relaciones AS
                WITH responde_a_cte AS (
                    SELECT
                        c.id AS comentario_id,
                        respuesta.tag AS responde_a_tag
                    FROM comentarios c
                    JOIN respuesta_comentario r ON r.respuesta_id = c.id
                    JOIN comentarios respuesta ON r.respondido_id = respuesta.id
                    WHERE c.status = 0 AND respuesta.status = 0
                ),
                respondido_por_cte AS (
                    SELECT
                        c.id AS comentario_id,
                        respuesta.tag AS respondido_por_tag
                    FROM comentarios c
                    JOIN respuesta_comentario r ON r.respondido_id = c.id
                    JOIN comentarios respuesta ON r.respuesta_id = respuesta.id
                    WHERE c.status = 0 AND respuesta.status = 0
                )

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
                    media.spoiler,
                    ra.responde_a_tag,
                    rp.respondido_por_tag
                FROM comentarios c
                JOIN hilos h ON h.id = c.hilo_id
                LEFT JOIN vw_spoileable_hashed_medias media ON media.id = c.media_id
                LEFT JOIN responde_a_cte ra ON ra.comentario_id = c.id
                LEFT JOIN respondido_por_cte rp ON rp.comentario_id = c.id
                WHERE c.status = 0;

                CREATE VIEW vw_comentarios_destacados AS
                SELECT
                    c.*
                FROM  vw_comentarios_con_relaciones c
                JOIN comentario_destacado d ON c.id = d.comentario_id
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_comentarios_con_relaciones;
                DROP VIEW IF EXISTS vw_comentarios_destacados_en_hilo;
            ");
        }
    }
}
