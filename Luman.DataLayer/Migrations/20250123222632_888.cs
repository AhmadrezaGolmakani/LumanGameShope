using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class _888 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_CategoryProduct_categoryProductId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_CategoryProduct_categoryProductId",
                table: "products");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_products_categoryProductId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_categories_categoryProductId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "categoryProductId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "categoryProductId",
                table: "categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryProductId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "categoryProductId",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    categoryProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => x.categoryProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_categoryProductId",
                table: "products",
                column: "categoryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_categoryProductId",
                table: "categories",
                column: "categoryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_CategoryProduct_categoryProductId",
                table: "categories",
                column: "categoryProductId",
                principalTable: "CategoryProduct",
                principalColumn: "categoryProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_CategoryProduct_categoryProductId",
                table: "products",
                column: "categoryProductId",
                principalTable: "CategoryProduct",
                principalColumn: "categoryProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
