using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyBookWeb.Migrations
{
    /// <inheritdoc />
    public partial class add_products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceList = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 10, "a" },
                    { 2, 20, "b" },
                    { 3, 30, "c" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Author", "Description", "ISBN", "Price", "Price100", "Price50", "PriceList", "Title" },
                values: new object[,]
                {
                    { 1, "Botza", "Dec1", "I1", 100.0, 85.0, 90.0, 95.0, "abc" },
                    { 2, "John Doe", "Dec2", "I2", 150.0, 135.0, 140.0, 145.0, "xyz" },
                    { 3, "Jane Smith", "Description for product 3", "I3", 120.0, 105.0, 110.0, 115.0, "Random Product 3" },
                    { 4, "Emily Clark", "Description for product 4", "I4", 200.0, 170.0, 180.0, 190.0, "Random Product 4" },
                    { 5, "Michael Brown", "Description for product 5", "I5", 175.0, 145.0, 155.0, 165.0, "Random Product 5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
