using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class usernameChiTietHoaDon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ChiTietHoaDons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "ChiTietHoaDons");
        }
    }
}
