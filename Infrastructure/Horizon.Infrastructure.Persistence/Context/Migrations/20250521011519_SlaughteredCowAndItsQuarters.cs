using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Infrastructure.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class SlaughteredCowAndItsQuarters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlaughteredCow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CowIdentifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SlaughteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaughteredCow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CowQuarter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    QuarterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlaughteredCowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowQuarter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CowQuarter_SlaughteredCow_SlaughteredCowId",
                        column: x => x.SlaughteredCowId,
                        principalTable: "SlaughteredCow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CowQuarter_SlaughteredCowId",
                table: "CowQuarter",
                column: "SlaughteredCowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CowQuarter");

            migrationBuilder.DropTable(
                name: "SlaughteredCow");
        }
    }
}
