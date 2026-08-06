using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateapi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "مدیریت" },
                    { 2, "کاربر عادی" },
                    { 3, "ادمین" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "RoleId",
                keyValue: 3);
        }
    }
}
