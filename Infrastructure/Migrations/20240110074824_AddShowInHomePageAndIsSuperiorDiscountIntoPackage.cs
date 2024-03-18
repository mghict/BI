using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShowInHomePageAndIsSuperiorDiscountIntoPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuperiorDiscount",
                table: "Packages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowInHomePage",
                table: "Packages",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuperiorDiscount",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ShowInHomePage",
                table: "Packages");
        }
    }
}
