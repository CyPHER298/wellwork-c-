using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WellworkGS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gestor",
                columns: table => new
                {
                    idGestor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_gestor = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    email_gestor = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    senha_gestor = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    cargo_gestor = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    departamento = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestor", x => x.idGestor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_usuario = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    email_usuario = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    senha_usuario = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    cargo_usuario = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    acessibilidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "ALERTACRISE",
                columns: table => new
                {
                    IDALERTACRISE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IDUSUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IDGESTOR = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATAHORA_ALERTA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS_ALERTA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERTACRISE", x => x.IDALERTACRISE);
                    table.ForeignKey(
                        name: "FK_ALERTACRISE_Gestor_IDGESTOR",
                        column: x => x.IDGESTOR,
                        principalTable: "Gestor",
                        principalColumn: "idGestor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ALERTACRISE_Usuario_IDUSUARIO",
                        column: x => x.IDUSUARIO,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LEMBRETE",
                columns: table => new
                {
                    IDLEMBRETE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IDUSUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_LEMBRETE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    FREQUENCIA = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ATIVO = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEMBRETE", x => x.IDLEMBRETE);
                    table.ForeignKey(
                        name: "FK_LEMBRETE_Usuario_IDUSUARIO",
                        column: x => x.IDUSUARIO,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "META",
                columns: table => new
                {
                    IDMETA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TITULO_META = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DESCRICAO_META = table.Column<string>(type: "NVARCHAR2(90)", maxLength: 90, nullable: false),
                    IDUSUARIO = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_META", x => x.IDMETA);
                    table.ForeignKey(
                        name: "FK_META_Usuario_IDUSUARIO",
                        column: x => x.IDUSUARIO,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "TAREFA",
                columns: table => new
                {
                    IDTAREFA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IDUSUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TITULO_TAREFA = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    DESCRICAO_TAREFA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DATAHORA_TAREFA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    STATUS_TAREFA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREFA", x => x.IDTAREFA);
                    table.ForeignKey(
                        name: "FK_TAREFA_Usuario_IDUSUARIO",
                        column: x => x.IDUSUARIO,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIMER",
                columns: table => new
                {
                    IDTIMER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IDUSUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_TIMER = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DURACAO = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    FIM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    STATUS_TIMER = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMER", x => x.IDTIMER);
                    table.ForeignKey(
                        name: "FK_TIMER_Usuario_IDUSUARIO",
                        column: x => x.IDUSUARIO,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALERTACRISE_IDGESTOR",
                table: "ALERTACRISE",
                column: "IDGESTOR");

            migrationBuilder.CreateIndex(
                name: "IX_ALERTACRISE_IDUSUARIO",
                table: "ALERTACRISE",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_Gestor_email_gestor",
                table: "Gestor",
                column: "email_gestor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LEMBRETE_IDUSUARIO",
                table: "LEMBRETE",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_META_IDUSUARIO",
                table: "META",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_TAREFA_IDUSUARIO",
                table: "TAREFA",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_TIMER_IDUSUARIO",
                table: "TIMER",
                column: "IDUSUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_email_usuario",
                table: "Usuario",
                column: "email_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALERTACRISE");

            migrationBuilder.DropTable(
                name: "LEMBRETE");

            migrationBuilder.DropTable(
                name: "META");

            migrationBuilder.DropTable(
                name: "TAREFA");

            migrationBuilder.DropTable(
                name: "TIMER");

            migrationBuilder.DropTable(
                name: "Gestor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
