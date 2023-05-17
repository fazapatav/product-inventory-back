using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sofka.ProductInventory.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBuy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Buy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Buy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
