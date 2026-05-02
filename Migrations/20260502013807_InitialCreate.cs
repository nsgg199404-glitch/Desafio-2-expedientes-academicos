using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpedientesAcademicos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumnos",
                columns: table => new
                {
                    AlumnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumnos", x => x.AlumnoId);
                });

            migrationBuilder.CreateTable(
                name: "materias",
                columns: table => new
                {
                    materiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMateria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materias", x => x.materiaId);
                });

            migrationBuilder.CreateTable(
                name: "expedientes",
                columns: table => new
                {
                    expedienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alumnoId = table.Column<int>(type: "int", nullable: false),
                    materiaId = table.Column<int>(type: "int", nullable: false),
                    NotaFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expedientes", x => x.expedienteId);
                    table.ForeignKey(
                        name: "FK_expedientes_alumnos_alumnoId",
                        column: x => x.alumnoId,
                        principalTable: "alumnos",
                        principalColumn: "AlumnoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expedientes_materias_materiaId",
                        column: x => x.materiaId,
                        principalTable: "materias",
                        principalColumn: "materiaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expedientes_alumnoId",
                table: "expedientes",
                column: "alumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_expedientes_materiaId",
                table: "expedientes",
                column: "materiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expedientes");

            migrationBuilder.DropTable(
                name: "alumnos");

            migrationBuilder.DropTable(
                name: "materias");
        }
    }
}
