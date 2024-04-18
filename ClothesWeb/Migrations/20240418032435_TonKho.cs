using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class TonKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TonKhos",
                columns: table => new
                {
                    MaTonKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_PhieuNhapKho = table.Column<int>(type: "int", nullable: false),
                    Id_Product = table.Column<int>(type: "int", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongTK = table.Column<int>(type: "int", nullable: false),
                    MucTonKhoToiThieu = table.Column<float>(type: "real", nullable: true),
                    GiaBan = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonKhos", x => x.MaTonKho);
                    table.ForeignKey(
                        name: "FK_TonKhos_PhieuNhapKhos_Id_PhieuNhapKho",
                        column: x => x.Id_PhieuNhapKho,
                        principalTable: "PhieuNhapKhos",
                        principalColumn: "Id_PhieuNhapKho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TonKhos_Products_Id_Product",
                        column: x => x.Id_Product,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TonKhos_Id_PhieuNhapKho",
                table: "TonKhos",
                column: "Id_PhieuNhapKho");

            migrationBuilder.CreateIndex(
                name: "IX_TonKhos_Id_Product",
                table: "TonKhos",
                column: "Id_Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TonKhos");
        }
    }
}
