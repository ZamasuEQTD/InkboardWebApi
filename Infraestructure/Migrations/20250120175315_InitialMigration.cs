using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moderador_name = table.Column<string>(type: "text", nullable: true),
                    registrado_en = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    oculto_por_defecto = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "encuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_encuestas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    filename = table.Column<string>(type: "text", nullable: true),
                    hash = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    provider = table.Column<string>(type: "text", nullable: false),
                    miniatura = table.Column<string>(type: "text", nullable: true),
                    previsualizacion = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "baneos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moderador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_baneado_id = table.Column<Guid>(type: "uuid", nullable: false),
                    concluye = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    razon = table.Column<int>(type: "integer", nullable: false),
                    mensaje = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_baneos", x => x.id);
                    table.ForeignKey(
                        name: "fk_baneos_asp_net_users_moderador_id",
                        column: x => x.moderador_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_baneos_asp_net_users_usuario_baneado_id",
                        column: x => x.usuario_baneado_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subcategorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    nombre_corto = table.Column<string>(type: "text", nullable: false),
                    categoria_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subcategorias", x => x.id);
                    table.ForeignKey(
                        name: "fk_subcategorias_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "respuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contenido = table.Column<string>(type: "text", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_respuestas", x => x.id);
                    table.ForeignKey(
                        name: "fk_respuestas_encuestas_encuesta_id",
                        column: x => x.encuesta_id,
                        principalTable: "encuestas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "votos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    votante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_votos", x => x.id);
                    table.ForeignKey(
                        name: "fk_votos_asp_net_users_votante_id",
                        column: x => x.votante_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_votos_encuestas_encuesta_id",
                        column: x => x.encuesta_id,
                        principalTable: "encuestas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medias_spoileables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hashed_media_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spoiler = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medias_spoileables", x => x.id);
                    table.ForeignKey(
                        name: "fk_medias_spoileables_medias_hashed_media_id",
                        column: x => x.hashed_media_id,
                        principalTable: "medias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hilos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    ultimo_bump = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    recibir_notificaciones = table.Column<bool>(type: "boolean", nullable: false),
                    dados_activado = table.Column<bool>(type: "boolean", nullable: false),
                    id_unico_activado = table.Column<bool>(type: "boolean", nullable: false),
                    portada_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subcategoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hilos", x => x.id);
                    table.ForeignKey(
                        name: "fk_hilos_asp_net_users_autor_id",
                        column: x => x.autor_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_hilos_encuestas_encuesta_id",
                        column: x => x.encuesta_id,
                        principalTable: "encuestas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_hilos_medias_spoileables_portada_id",
                        column: x => x.portada_id,
                        principalTable: "medias_spoileables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_hilos_subcategorias_subcategoria_id",
                        column: x => x.subcategoria_id,
                        principalTable: "subcategorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    recibir_notificaciones = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false),
                    tag = table.Column<string>(type: "text", nullable: false),
                    dados = table.Column<int>(type: "integer", nullable: true),
                    tag_unico = table.Column<string>(type: "text", nullable: true),
                    media_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentarios_asp_net_users_autor_id",
                        column: x => x.autor_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_medias_spoileables_media_id",
                        column: x => x.media_id,
                        principalTable: "medias_spoileables",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "denuncia_hilo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    razon = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    denunciante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_denuncia_hilo", x => x.id);
                    table.ForeignKey(
                        name: "fk_denuncia_hilo_asp_net_users_denunciante_id",
                        column: x => x.denunciante_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_denuncia_hilo_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hilo_interaccion",
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

            migrationBuilder.CreateTable(
                name: "stickies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stickies", x => x.id);
                    table.ForeignKey(
                        name: "fk_stickies_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentario_destacado",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentario_destacado", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentario_destacado_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentario_destacado_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentario_interracion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    oculto = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentario_interracion", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentario_interracion_asp_net_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentario_interracion_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "denuncia_de_comentario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    razon = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    denunciante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_denuncia_de_comentario", x => x.id);
                    table.ForeignKey(
                        name: "fk_denuncia_de_comentario_asp_net_users_denunciante_id",
                        column: x => x.denunciante_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_denuncia_de_comentario_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notificaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    notificado_id = table.Column<Guid>(type: "uuid", nullable: false),
                    leida = table.Column<bool>(type: "boolean", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_de_interaccion = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    respondido_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notificaciones", x => x.id);
                    table.ForeignKey(
                        name: "fk_notificaciones_asp_net_users_notificado_id",
                        column: x => x.notificado_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notificaciones_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notificaciones_comentarios_respondido_id",
                        column: x => x.respondido_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notificaciones_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "respuesta_comentario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    respondido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_respuesta_comentario", x => x.id);
                    table.ForeignKey(
                        name: "fk_respuesta_comentario_comentarios_respondido_id",
                        column: x => x.respondido_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_respuesta_comentario_comentarios_respuesta_id",
                        column: x => x.respuesta_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_baneos_moderador_id",
                table: "baneos",
                column: "moderador_id");

            migrationBuilder.CreateIndex(
                name: "ix_baneos_usuario_baneado_id",
                table: "baneos",
                column: "usuario_baneado_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentario_destacado_comentario_id",
                table: "comentario_destacado",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentario_destacado_hilo_id",
                table: "comentario_destacado",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentario_interracion_comentario_id",
                table: "comentario_interracion",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentario_interracion_usuario_id",
                table: "comentario_interracion",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_autor_id",
                table: "comentarios",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_hilo_id",
                table: "comentarios",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_media_id",
                table: "comentarios",
                column: "media_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_denuncia_de_comentario_comentario_id",
                table: "denuncia_de_comentario",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "ix_denuncia_de_comentario_denunciante_id",
                table: "denuncia_de_comentario",
                column: "denunciante_id");

            migrationBuilder.CreateIndex(
                name: "ix_denuncia_hilo_denunciante_id",
                table: "denuncia_hilo",
                column: "denunciante_id");

            migrationBuilder.CreateIndex(
                name: "ix_denuncia_hilo_hilo_id",
                table: "denuncia_hilo",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_hilo_interaccion_hilo_id",
                table: "hilo_interaccion",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_hilo_interaccion_usuario_id",
                table: "hilo_interaccion",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_hilos_autor_id",
                table: "hilos",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "ix_hilos_encuesta_id",
                table: "hilos",
                column: "encuesta_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_hilos_portada_id",
                table: "hilos",
                column: "portada_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_hilos_subcategoria_id",
                table: "hilos",
                column: "subcategoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_medias_hash",
                table: "medias",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_medias_spoileables_hashed_media_id",
                table: "medias_spoileables",
                column: "hashed_media_id");

            migrationBuilder.CreateIndex(
                name: "ix_notificaciones_comentario_id",
                table: "notificaciones",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "ix_notificaciones_hilo_id",
                table: "notificaciones",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "ix_notificaciones_notificado_id",
                table: "notificaciones",
                column: "notificado_id");

            migrationBuilder.CreateIndex(
                name: "ix_notificaciones_respondido_id",
                table: "notificaciones",
                column: "respondido_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuesta_comentario_respondido_id",
                table: "respuesta_comentario",
                column: "respondido_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuesta_comentario_respuesta_id",
                table: "respuesta_comentario",
                column: "respuesta_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id");

            migrationBuilder.CreateIndex(
                name: "ix_stickies_hilo_id",
                table: "stickies",
                column: "hilo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_subcategorias_categoria_id",
                table: "subcategorias",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "ix_votos_encuesta_id",
                table: "votos",
                column: "encuesta_id");

            migrationBuilder.CreateIndex(
                name: "ix_votos_votante_id",
                table: "votos",
                column: "votante_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "baneos");

            migrationBuilder.DropTable(
                name: "comentario_destacado");

            migrationBuilder.DropTable(
                name: "comentario_interracion");

            migrationBuilder.DropTable(
                name: "denuncia_de_comentario");

            migrationBuilder.DropTable(
                name: "denuncia_hilo");

            migrationBuilder.DropTable(
                name: "hilo_interaccion");

            migrationBuilder.DropTable(
                name: "notificaciones");

            migrationBuilder.DropTable(
                name: "respuesta_comentario");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "stickies");

            migrationBuilder.DropTable(
                name: "votos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "hilos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "encuestas");

            migrationBuilder.DropTable(
                name: "medias_spoileables");

            migrationBuilder.DropTable(
                name: "subcategorias");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
