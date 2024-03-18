using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class purches01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Subscriptions_SubscriptionId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Users_UserId",
                table: "Purches");

            migrationBuilder.DropColumn(
                name: "IsFinal",
                table: "Purches");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Purches",
                newName: "PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Purches_UserId",
                table: "Purches",
                newName: "IX_Purches_PackageId");

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

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Purches",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PersonId",
                table: "Subscriptions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Purches_OwnerId",
                table: "Purches",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Packages_PackageId",
                table: "Purches",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Persons_OwnerId",
                table: "Purches",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Subscriptions_SubscriptionId",
                table: "Purches",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Persons_PersonId",
                table: "Subscriptions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Packages_PackageId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Persons_OwnerId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Subscriptions_SubscriptionId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Persons_PersonId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PersonId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Purches_OwnerId",
                table: "Purches");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Purches");

            migrationBuilder.RenameColumn(
                name: "PackageId",
                table: "Purches",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Purches_PackageId",
                table: "Purches",
                newName: "IX_Purches_UserId");

            migrationBuilder.AlterColumn<long>(
                name: "SubscriptionId",
                table: "Purches",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinal",
                table: "Purches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Subscriptions_SubscriptionId",
                table: "Purches",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Users_UserId",
                table: "Purches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
