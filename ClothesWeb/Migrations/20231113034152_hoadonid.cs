using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class hoadonid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_Products_SanPhamId",
                table: "ChiTietHoaDons");

            migrationBuilder.RenameColumn(
                name: "SanPhamId",
                table: "ChiTietHoaDons",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_SanPhamId",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_Products_ProductId",
                table: "ChiTietHoaDons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_Products_ProductId",
                table: "ChiTietHoaDons");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ChiTietHoaDons",
                newName: "SanPhamId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDons_ProductId",
                table: "ChiTietHoaDons",
                newName: "IX_ChiTietHoaDons_SanPhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_Products_SanPhamId",
                table: "ChiTietHoaDons",
                column: "SanPhamId",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
