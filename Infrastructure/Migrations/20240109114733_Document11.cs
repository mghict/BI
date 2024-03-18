using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moneyon.PowerBi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Document11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentContent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentContent_Documents_Id",
                        column: x => x.Id,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId",
                table: "Documents",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentContent");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
