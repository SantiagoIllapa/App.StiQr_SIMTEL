using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StiQr_SIMTEL.Server.Migrations
{
    /// <inheritdoc />
    public partial class adding_Transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentName",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "AgentDescription",
                table: "Agents",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAgent = table.Column<int>(type: "int", nullable: false),
                    IdLabelQr = table.Column<int>(type: "int", nullable: false),
                    DateMark = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Agents",
                newName: "AgentDescription");

            migrationBuilder.AddColumn<string>(
                name: "AgentName",
                table: "Agents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
