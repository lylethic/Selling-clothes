using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesWeb.Migrations
{
    /// <inheritdoc />
    public partial class NCC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NhaCungCapIdNhaCungCap",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    IdNhaCungCap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NguoiLienLac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.IdNhaCungCap);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_NhaCungCapIdNhaCungCap",
                table: "Products",
                column: "NhaCungCapIdNhaCungCap");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_NhaCungCaps_NhaCungCapIdNhaCungCap",
                table: "Products",
                column: "NhaCungCapIdNhaCungCap",
                principalTable: "NhaCungCaps",
                principalColumn: "IdNhaCungCap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_NhaCungCaps_NhaCungCapIdNhaCungCap",
                table: "Products");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

            migrationBuilder.DropIndex(
                name: "IX_Products_NhaCungCapIdNhaCungCap",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NhaCungCapIdNhaCungCap",
                table: "Products");
        }
    }
}
