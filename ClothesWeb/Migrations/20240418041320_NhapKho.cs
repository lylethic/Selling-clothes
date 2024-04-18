using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class NhapKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhapKhos",
                columns: table => new
                {
                    Id_NhapKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayNhapKho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNCC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TongTienHang = table.Column<double>(type: "float", nullable: false),
                    Id_LoaiHangHoa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKhos", x => x.Id_NhapKho);
                    table.ForeignKey(
                        name: "FK_NhapKhos_Categories_Id_LoaiHangHoa",
                        column: x => x.Id_LoaiHangHoa,
                        principalTable: "Categories",
                        principalColumn: "LoaiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhapKhos_NhaCungCaps_IdNCC",
                        column: x => x.IdNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "IdNhaCungCap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_Id_LoaiHangHoa",
                table: "NhapKhos",
                column: "Id_LoaiHangHoa");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_IdNCC",
                table: "NhapKhos",
                column: "IdNCC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhapKhos");
        }
    }
}
