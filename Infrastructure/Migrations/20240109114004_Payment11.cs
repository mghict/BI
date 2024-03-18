using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Payment11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Subscriptions");
        }
    }
}
