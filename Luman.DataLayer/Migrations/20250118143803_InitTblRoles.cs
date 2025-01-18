using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitTblRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_users_UserId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "userRoles");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "roles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                table: "userRoles",
                newName: "IX_userRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "userRoles",
                newName: "IX_userRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles",
                column: "RU_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_roles_RoleId",
                table: "userRoles",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_users_UserId",
                table: "userRoles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_roles_RoleId",
                table: "userRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_users_UserId",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.RenameTable(
                name: "userRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_userRoles_UserId",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "RU_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_users_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
