using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Pendencia");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Pendencia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Pendencia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ResponsavelId",
                table: "Pendencia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Responsavel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsavel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pendencia_ResponsavelId",
                table: "Pendencia",
                column: "ResponsavelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pendencia_Responsavel_ResponsavelId",
                table: "Pendencia",
                column: "ResponsavelId",
                principalTable: "Responsavel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pendencia_Responsavel_ResponsavelId",
                table: "Pendencia");

            migrationBuilder.DropTable(
                name: "Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_Pendencia_ResponsavelId",
                table: "Pendencia");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Pendencia");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Pendencia");

            migrationBuilder.DropColumn(
                name: "ResponsavelId",
                table: "Pendencia");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Pendencia",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
