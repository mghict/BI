using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Account11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Packages_PackageId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PackageId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Type",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Type",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Type",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "PackageId",
                table: "Payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Type",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PackageId",
                table: "Payments",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Packages_PackageId",
                table: "Payments",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }
    }
}
