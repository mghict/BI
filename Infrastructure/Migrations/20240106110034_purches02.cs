using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class purches02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Packages_PackageId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Subscriptions_SubscriptionId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Persons_PersonId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PersonId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PurchesId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Purches_SubscriptionId",
                table: "Purches");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Subscriptions",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_OwnerId");

            migrationBuilder.AlterColumn<long>(
                name: "SubscriptionId",
                table: "Purches",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PackageId",
                table: "Purches",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PurchesId",
                table: "Subscriptions",
                column: "PurchesId",
                unique: true,
                filter: "[PurchesId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Packages_PackageId",
                table: "Purches",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Persons_OwnerId",
                table: "Subscriptions",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Packages_PackageId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Persons_OwnerId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PurchesId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Subscriptions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_OwnerId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_UserId");

            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "Subscriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubscriptionId",
                table: "Purches",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PackageId",
                table: "Purches",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PersonId",
                table: "Subscriptions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PurchesId",
                table: "Subscriptions",
                column: "PurchesId");

            migrationBuilder.CreateIndex(
                name: "IX_Purches_SubscriptionId",
                table: "Purches",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Packages_PackageId",
                table: "Purches",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Subscriptions_SubscriptionId",
                table: "Purches",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Persons_PersonId",
                table: "Subscriptions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
