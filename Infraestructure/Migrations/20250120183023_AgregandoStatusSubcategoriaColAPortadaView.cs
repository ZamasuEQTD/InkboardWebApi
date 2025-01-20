using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoStatusSubcategoriaColAPortadaView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS HiloPortadaView;
                CREATE VIEW HiloPortadaView AS
                SELECT
                    hilo.id,
                    hilo.status,
                    hilo.titulo,
                    hilo.autor_id,
                    hilo.recibir_notificaciones,
                    hilo.created_at,
                    hilo.ultimo_bump,
                    hilo.subcategoria_id,
                    subcategoria.nombre_corto AS subcategoria,
                    sticky.id IS NOT NULL AS es_sticky,
                    hilo.encuesta_id IS NOT NULL AS tiene_encuesta,
                    hilo.dados_activado,
                    hilo.id_unico_activado,
                    spoileable.spoiler,
                    portada.miniatura as url
                FROM hilos hilo
                    JOIN medias_spoileables spoileable ON spoileable.id = hilo.portada_id
                    JOIN medias portada ON portada.id = spoileable.hashed_media_id
                    LEFT JOIN stickies sticky ON hilo.id = sticky.hilo_id
                    JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
