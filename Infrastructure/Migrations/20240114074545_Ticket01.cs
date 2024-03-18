using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ticket01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TicketDetailId",
                table: "Documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketCategories_TicketCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TicketCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingCode = table.Column<long>(type: "bigint", nullable: false),
                    TicketCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUserModifiedId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Persons_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Persons_LastUserModifiedId",
                        column: x => x.LastUserModifiedId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketCategories_TicketCategoryId",
                        column: x => x.TicketCategoryId,
                        principalTable: "TicketCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction,
                        onUpdate: ReferentialAction.NoAction);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TicketDetailId",
                table: "Documents",
                column: "TicketDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCategories_ParentId",
                table: "TicketCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_PersonId",
                table: "TicketDetails",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatedById",
                table: "Tickets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LastUserModifiedId",
                table: "Tickets",
                column: "LastUserModifiedId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketCategoryId",
                table: "Tickets",
                column: "TicketCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_TicketDetails_TicketDetailId",
                table: "Documents",
                column: "TicketDetailId",
                principalTable: "TicketDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_TicketDetails_TicketDetailId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketCategories");

            migrationBuilder.DropIndex(
                name: "IX_Documents_TicketDetailId",
                table: "Documents");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DropColumn(
                name: "TicketDetailId",
                table: "Documents");
        }
    }
}
