using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CreandoEncuestaMediaViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_encuestas;
CREATE VIEW vw_encuestas AS
SELECT 
	encuesta.id,
  respuesta.id AS respuesta_id,
  respuesta.contenido as respuesta,
	(SELECT count(id) AS votos FROM votos WHERE respuesta_id = respuesta.id)
FROM encuestas encuesta
JOIN respuestas respuesta ON respuesta.encuesta_id = encuesta.id;

DROP VIEW IF EXISTS vw_spoileable_hashed_medias;
CREATE VIEW vw_spoileable_hashed_medias AS
SELECT
	spoileable.id,
  media.url,
  media.provider,
  media.previsualizacion,
  spoileable.spoiler
FROM medias_spoileables spoileable
JOIN medias media ON media.id = spoileable.hashed_media_id;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_encuestas; 
                DROP VIEW IF EXISTS vw_spoileable_hashed_medias;
                 ");
        }
    }
}
