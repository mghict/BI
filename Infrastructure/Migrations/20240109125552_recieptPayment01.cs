using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class recieptPayment01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptPayerName",
                table: "Payments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ImageId",
                table: "Payments",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Documents_ImageId",
                table: "Payments",
                column: "ImageId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Documents_ImageId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ImageId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReceiptPayerName",
                table: "Payments");
        }
    }
}
