using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class EditeTBLpro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_CategoryProduct_CategoryId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "products",
                newName: "Cat_ProCa_PrId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "products",
                newName: "IX_products_Cat_ProCa_PrId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cat_ProCa_PrId",
                table: "CategoryProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cat_Pro",
                columns: table => new
                {
                    Ca_PrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_Pro", x => x.Ca_PrId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Cat_Pro_Cat_ProCa_PrId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Cat_Pro_Cat_ProCa_PrId",
                table: "products");

            migrationBuilder.DropTable(
                name: "Cat_Pro");

            migrationBuilder.DropIndex(
                name: "IX_CategoryProduct_Cat_ProCa_PrId",
                table: "CategoryProduct");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Cat_ProCa_PrId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "Cat_ProCa_PrId",
                table: "products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_products_Cat_ProCa_PrId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_CategoryProduct_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "CategoryProduct",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
