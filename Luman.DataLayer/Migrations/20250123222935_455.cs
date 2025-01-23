using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class _455 : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "categoryProductId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "categoryProductId",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categoryProducts_categoryProductId",
                table: "categories",
                column: "categoryProductId",
                principalTable: "categoryProducts",
                principalColumn: "categoryProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categoryProducts_categoryProductId",
                table: "products",
                column: "categoryProductId",
                principalTable: "categoryProducts",
                principalColumn: "categoryProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categoryProducts_categoryProductId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categoryProducts_categoryProductId",
                table: "products");

            migrationBuilder.AlterColumn<int>(
                name: "categoryProductId",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "categoryProductId",
                table: "categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
