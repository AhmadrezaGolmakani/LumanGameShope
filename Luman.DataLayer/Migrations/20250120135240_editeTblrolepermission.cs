using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class editeTblrolepermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rolePermissions_permitions_permitionPermissionID",
                table: "rolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_rolePermissions_permitionPermissionID",
                table: "rolePermissions");

            migrationBuilder.DropColumn(
                name: "permitionPermissionID",
                table: "rolePermissions");

            migrationBuilder.CreateIndex(
                name: "IX_rolePermissions_PermissionID",
                table: "rolePermissions",
                column: "PermissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_rolePermissions_permitions_PermissionID",
                table: "rolePermissions",
                column: "PermissionID",
                principalTable: "permitions",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rolePermissions_permitions_PermissionID",
                table: "rolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_rolePermissions_PermissionID",
                table: "rolePermissions");

            migrationBuilder.AddColumn<int>(
                name: "permitionPermissionID",
                table: "rolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_rolePermissions_permitionPermissionID",
                table: "rolePermissions",
                column: "permitionPermissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_rolePermissions_permitions_permitionPermissionID",
                table: "rolePermissions",
                column: "permitionPermissionID",
                principalTable: "permitions",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
