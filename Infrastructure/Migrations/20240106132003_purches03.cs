using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class purches03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Accounts_AccountId1",
                table: "TransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId1",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_AccountId1",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionId1",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                table: "TransactionDetails");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Purches",
                newName: "CreateOn");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionId",
                table: "TransactionDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "AccountId",
                table: "TransactionDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Purches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransactionId",
                table: "Purches",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "Packages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_AccountId",
                table: "TransactionDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purches_TransactionId",
                table: "Purches",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purches_Transactions_TransactionId",
                table: "Purches",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Accounts_AccountId",
                table: "TransactionDetails",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purches_Transactions_TransactionId",
                table: "Purches");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Accounts_AccountId",
                table: "TransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_AccountId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_Purches_TransactionId",
                table: "Purches");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Purches");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Purches");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Purches",
                newName: "CreateDate");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TransactionDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AccountId1",
                table: "TransactionDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TransactionId1",
                table: "TransactionDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Packages",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_AccountId1",
                table: "TransactionDetails",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId1",
                table: "TransactionDetails",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Accounts_AccountId1",
                table: "TransactionDetails",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId1",
                table: "TransactionDetails",
                column: "TransactionId1",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
