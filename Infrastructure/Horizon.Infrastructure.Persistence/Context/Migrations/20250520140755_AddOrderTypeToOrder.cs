using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizon.Infrastructure.Persistence.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTypeToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "JobOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "JobOrders");
        }
    }
}
