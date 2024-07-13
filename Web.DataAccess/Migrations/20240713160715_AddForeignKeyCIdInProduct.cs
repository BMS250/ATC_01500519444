using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyCIdInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "CId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "CId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "CId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "CId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "CId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CId",
                table: "Products",
                column: "CId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CId",
                table: "Products",
                column: "CId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CId",
                table: "Products");
        }
    }
}
