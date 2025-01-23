using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditeTBLpro3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Pro_CategoryProduct_CategoriesCategoryId",
                table: "Cat_Pro");

            migrationBuilder.DropIndex(
                name: "IX_Cat_Pro_CategoriesCategoryId",
                table: "Cat_Pro");

            migrationBuilder.DropColumn(
                name: "CategoriesCategoryId",
                table: "Cat_Pro");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Pro_CategoryId",
                table: "Cat_Pro",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Pro_CategoryProduct_CategoryId",
                table: "Cat_Pro",
                column: "CategoryId",
                principalTable: "CategoryProduct",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Pro_CategoryProduct_CategoryId",
                table: "Cat_Pro");

            migrationBuilder.DropIndex(
                name: "IX_Cat_Pro_CategoryId",
                table: "Cat_Pro");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Pro_CategoryProduct_CategoriesCategoryId",
                table: "Cat_Pro",
                column: "CategoriesCategoryId",
                principalTable: "CategoryProduct",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
