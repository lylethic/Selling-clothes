using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMethod_HoaDons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayMentMethod",
                table: "HoaDons",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayMentMethod",
                table: "HoaDons");
        }
    }
}
