using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditePro6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat_Pro");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_categories_ProductId",
                table: "categories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_products_ProductId",
                table: "categories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_products_ProductId",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_ProductId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "categories");

            migrationBuilder.CreateTable(
                name: "Cat_Pro",
                columns: table => new
                {
                    Ca_PrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Pro", x => x.Ca_PrId);
                    table.ForeignKey(
                        name: "FK_Cat_Pro_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cat_Pro_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Pro_CategoryId",
                table: "Cat_Pro",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cat_Pro_ProductId",
                table: "Cat_Pro",
                column: "ProductId");
        }
    }
}
