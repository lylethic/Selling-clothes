using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class ChiTietPhieuNhapKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhapKhos",
                columns: table => new
                {
                    Id_CTPNKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_PhieuNhapKho = table.Column<int>(type: "int", nullable: false),
                    Id_Product = table.Column<int>(type: "int", nullable: false),
                    SoLuongNhapKho = table.Column<int>(type: "int", nullable: false),
                    GiaNhapSanPham = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhapKhos", x => x.Id_CTPNKho);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhapKhos_PhieuNhapKhos_Id_PhieuNhapKho",
                        column: x => x.Id_PhieuNhapKho,
                        principalTable: "PhieuNhapKhos",
                        principalColumn: "Id_PhieuNhapKho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhapKhos_Products_Id_Product",
                        column: x => x.Id_Product,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhapKhos_Id_PhieuNhapKho",
                table: "ChiTietPhieuNhapKhos",
                column: "Id_PhieuNhapKho");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhapKhos_Id_Product",
                table: "ChiTietPhieuNhapKhos",
                column: "Id_Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhapKhos");
        }
    }
}
