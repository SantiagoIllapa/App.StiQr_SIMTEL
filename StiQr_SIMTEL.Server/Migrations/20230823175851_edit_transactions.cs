using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StiQr_SIMTEL.Server.Migrations
{
    /// <inheritdoc />
    public partial class edit_transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateMark",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "IdAgent",
                table: "Transactions",
                newName: "IdUserTransmiter");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTransacction",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Transactions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTransacction",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "IdUserTransmiter",
                table: "Transactions",
                newName: "IdAgent");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMark",
                table: "Transactions",
                type: "datetime2",
                nullable: true);
        }
    }
}
