using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace squeletteimplantation.Migrations
{
    public partial class Youhou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domaine",
                columns: table => new
                {
                    DomId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DomNom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domaine", x => x.DomId);
                });

            migrationBuilder.CreateTable(
                name: "Machin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NombreMagique = table.Column<int>(nullable: false),
                    Truc = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trace",
                columns: table => new
                {
                    TracId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TracUrl = table.Column<string>(nullable: false),
                    TraceNom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trace", x => x.TracId);
                });

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    CatId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CatNom = table.Column<string>(nullable: false),
                    DomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.CatId);
                    table.ForeignKey(
                        name: "fk_cat_dom",
                        column: x => x.DomId,
                        principalTable: "Domaine",
                        principalColumn: "DomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Critere",
                columns: table => new
                {
                    CritId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CatId = table.Column<int>(nullable: false),
                    CritNom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Critere", x => x.CritId);
                    table.ForeignKey(
                        name: "fk_crit_cat",
                        column: x => x.CatId,
                        principalTable: "Categorie",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelTracCrit",
                columns: table => new
                {
                    CritId = table.Column<int>(nullable: false),
                    TracId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelTracCrit", x => new { x.CritId, x.TracId });
                    table.ForeignKey(
                        name: "fk_Critere_relTracCrit",
                        column: x => x.CritId,
                        principalTable: "Critere",
                        principalColumn: "CritId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Trace_relTracCrit",
                        column: x => x.TracId,
                        principalTable: "Trace",
                        principalColumn: "TracId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorie_DomId",
                table: "Categorie",
                column: "DomId");

            migrationBuilder.CreateIndex(
                name: "IX_Critere_CatId",
                table: "Critere",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_RelTracCrit_TracId",
                table: "RelTracCrit",
                column: "TracId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Machin");

            migrationBuilder.DropTable(
                name: "RelTracCrit");

            migrationBuilder.DropTable(
                name: "Critere");

            migrationBuilder.DropTable(
                name: "Trace");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Domaine");
        }
    }
}
