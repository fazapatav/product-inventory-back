using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sofka.ProductInventory.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProdcutBuy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBuy_Buy_BuyId",
                table: "ProductBuy");

            migrationBuilder.AlterColumn<int>(
                name: "BuyId",
                table: "ProductBuy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBuy_Buy_BuyId",
                table: "ProductBuy",
                column: "BuyId",
                principalTable: "Buy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBuy_Buy_BuyId",
                table: "ProductBuy");

            migrationBuilder.AlterColumn<int>(
                name: "BuyId",
                table: "ProductBuy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBuy_Buy_BuyId",
                table: "ProductBuy",
                column: "BuyId",
                principalTable: "Buy",
                principalColumn: "Id");
        }
    }
}
