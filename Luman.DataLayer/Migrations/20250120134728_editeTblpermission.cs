using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class editeTblpermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "permitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_permitions_ParentID",
                table: "permitions",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_permitions_permitions_ParentID",
                table: "permitions",
                column: "ParentID",
                principalTable: "permitions",
                principalColumn: "PermissionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permitions_permitions_ParentID",
                table: "permitions");

            migrationBuilder.DropIndex(
                name: "IX_permitions_ParentID",
                table: "permitions");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "permitions");
        }
    }
}
