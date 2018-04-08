using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace squeletteimplantation.Migrations
{
    public partial class TablePourHistorique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelTracUsag",
                columns: table => new
                {
                    TracId = table.Column<int>(nullable: false),
                    UtilId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelTracUsag", x => new { x.TracId, x.UtilId });
                    table.ForeignKey(
                        name: "fk_Trace_relTracUsag",
                        column: x => x.TracId,
                        principalTable: "Trace",
                        principalColumn: "TracId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Utilisateur_relTracUsag",
                        column: x => x.UtilId,
                        principalTable: "Utilisateur",
                        principalColumn: "UtilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelTracUsag_UtilId",
                table: "RelTracUsag",
                column: "UtilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelTracUsag");
        }
    }
}
