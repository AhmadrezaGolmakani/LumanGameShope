using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class _21321 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_products_pId",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "pId",
                table: "categories",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_pId",
                table: "categories",
                newName: "IX_categories_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_products_ProductId",
                table: "categories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_products_ProductId",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "categories",
                newName: "pId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_ProductId",
                table: "categories",
                newName: "IX_categories_pId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_products_pId",
                table: "categories",
                column: "pId",
                principalTable: "products",
                principalColumn: "ProductId");
        }
    }
}
