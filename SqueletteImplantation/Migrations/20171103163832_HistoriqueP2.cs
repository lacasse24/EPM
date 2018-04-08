using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace squeletteimplantation.Migrations
{
    public partial class HistoriqueP2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelTracUsag",
                table: "RelTracUsag");

            migrationBuilder.RenameTable(
                name: "RelTracUsag",
                newName: "RelTracUsager");

            migrationBuilder.RenameIndex(
                name: "IX_RelTracUsag_UtilId",
                table: "RelTracUsager",
                newName: "IX_RelTracUsager_UtilId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTelechargement",
                table: "RelTracUsager",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelTracUsager",
                table: "RelTracUsager",
                columns: new[] { "TracId", "UtilId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelTracUsager",
                table: "RelTracUsager");

            migrationBuilder.DropColumn(
                name: "DateTelechargement",
                table: "RelTracUsager");

            migrationBuilder.RenameTable(
                name: "RelTracUsager",
                newName: "RelTracUsag");

            migrationBuilder.RenameIndex(
                name: "IX_RelTracUsager_UtilId",
                table: "RelTracUsag",
                newName: "IX_RelTracUsag_UtilId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelTracUsag",
                table: "RelTracUsag",
                columns: new[] { "TracId", "UtilId" });
        }
    }
}
