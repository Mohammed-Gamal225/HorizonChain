using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Infrastructure.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddJobOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "JobOrders",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JobOrders");
        }
    }
}
