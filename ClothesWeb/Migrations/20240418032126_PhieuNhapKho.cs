using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class PhieuNhapKho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhieuNhapKhos",
                columns: table => new
                {
                    Id_PhieuNhapKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianNhapKho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_NhaCungCap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TongTienPhieuNhapKho = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhapKhos", x => x.Id_PhieuNhapKho);
                    table.ForeignKey(
                        name: "FK_PhieuNhapKhos_NhaCungCaps_Id_NhaCungCap",
                        column: x => x.Id_NhaCungCap,
                        principalTable: "NhaCungCaps",
                        principalColumn: "IdNhaCungCap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhapKhos_Id_NhaCungCap",
                table: "PhieuNhapKhos",
                column: "Id_NhaCungCap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhieuNhapKhos");
        }
    }
}
