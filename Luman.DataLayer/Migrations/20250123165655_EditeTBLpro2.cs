using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditeTBLpro2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Cat_Pro_Cat_ProCa_PrId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Cat_Pro_Cat_ProCa_PrId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_Cat_ProCa_PrId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_CategoryProduct_Cat_ProCa_PrId",
                table: "CategoryProduct");

            migrationBuilder.DropColumn(
                name: "Cat_ProCa_PrId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Cat_ProCa_PrId",
                table: "CategoryProduct");

            migrationBuilder.AddColumn<int>(
                name: "CategoriesCategoryId",
                table: "Cat_Pro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Pro_CategoriesCategoryId",
                table: "Cat_Pro",
                column: "CategoriesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Pro_ProductId",
                table: "Cat_Pro",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Pro_CategoryProduct_CategoriesCategoryId",
                table: "Cat_Pro",
                column: "CategoriesCategoryId",
                principalTable: "CategoryProduct",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Pro_products_ProductId",
                table: "Cat_Pro",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Pro_CategoryProduct_CategoriesCategoryId",
                table: "Cat_Pro");

            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Pro_products_ProductId",
                table: "Cat_Pro");

            migrationBuilder.DropIndex(
                name: "IX_Cat_Pro_CategoriesCategoryId",
                table: "Cat_Pro");

            migrationBuilder.DropIndex(
                name: "IX_Cat_Pro_ProductId",
                table: "Cat_Pro");

            migrationBuilder.DropColumn(
                name: "CategoriesCategoryId",
                table: "Cat_Pro");

            migrationBuilder.AddColumn<int>(
                name: "Cat_ProCa_PrId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cat_ProCa_PrId",
                table: "CategoryProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_Cat_ProCa_PrId",
                table: "products",
                column: "Cat_ProCa_PrId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_Cat_ProCa_PrId",
                table: "CategoryProduct",
                column: "Cat_ProCa_PrId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Cat_Pro_Cat_ProCa_PrId",
                table: "CategoryProduct",
                column: "Cat_ProCa_PrId",
                principalTable: "Cat_Pro",
                principalColumn: "Ca_PrId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_Cat_Pro_Cat_ProCa_PrId",
                table: "products",
                column: "Cat_ProCa_PrId",
                principalTable: "Cat_Pro",
                principalColumn: "Ca_PrId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
