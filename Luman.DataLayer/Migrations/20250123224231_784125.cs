using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class _784125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categoryProducts_categoryProductId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categoryProducts_categoryProductId",
                table: "products");

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

            migrationBuilder.RenameColumn(
                name: "categoryProductId",
                table: "categoryProducts",
                newName: "CategoryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_categoryProducts_CategoryId",
                table: "categoryProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categoryProducts_ProductId",
                table: "categoryProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_categoryProducts_categories_CategoryId",
                table: "categoryProducts",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categoryProducts_products_ProductId",
                table: "categoryProducts",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryProducts_categories_CategoryId",
                table: "categoryProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_categoryProducts_products_ProductId",
                table: "categoryProducts");

            migrationBuilder.DropIndex(
                name: "IX_categoryProducts_CategoryId",
                table: "categoryProducts");

            migrationBuilder.DropIndex(
                name: "IX_categoryProducts_ProductId",
                table: "categoryProducts");

            migrationBuilder.RenameColumn(
                name: "CategoryProductId",
                table: "categoryProducts",
                newName: "categoryProductId");

            migrationBuilder.AddColumn<int>(
                name: "categoryProductId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "categoryProductId",
                table: "categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_categoryProductId",
                table: "products",
                column: "categoryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_categoryProductId",
                table: "categories",
                column: "categoryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categoryProducts_categoryProductId",
                table: "categories",
                column: "categoryProductId",
                principalTable: "categoryProducts",
                principalColumn: "categoryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_categoryProducts_categoryProductId",
                table: "products",
                column: "categoryProductId",
                principalTable: "categoryProducts",
                principalColumn: "categoryProductId");
        }
    }
}
