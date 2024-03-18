using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class purches04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "PermisionId", "ReportId", "Type" },
                values: new object[,]
                {
                    { 1L, "نمایش بسته های اشتراک", "PackagesView", 200, null, 2 },
                    { 2L, "ایجاد بسته های اشتراک", "PackagesCreate", 201, null, 2 },
                    { 3L, "ویرایش بسته های اشتراک", "PackagesEdit", 202, null, 2 },
                    { 4L, "نمایش نقشها", "RolesView", 210, null, 2 },
                    { 5L, "ایجاد نقش", "RolesCreate", 211, null, 2 },
                    { 6L, "ویرایش نقش", "RolesEdit", 212, null, 2 },
                    { 7L, "نمایش کاربران", "UsersView", 220, null, 2 },
                    { 8L, "تغییر رمزعبور کاربران", "UsersResetPassword", 221, null, 2 },
                    { 9L, "تغییر نقش کاربران", "UsersChangeRoles", 222, null, 2 },
                    { 10L, "نمایش گزارشات", "ReportView", 230, null, 1 },
                    { 11L, "ایجاد گزارشات", "ReportCreate", 231, null, 1 },
                    { 12L, "ویرایش گزارشات", "ReportEdit", 232, null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12L);
        }
    }
}
