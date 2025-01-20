using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luman.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class editeuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RU_Id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RU_Id",
                table: "users");
        }
    }
}
