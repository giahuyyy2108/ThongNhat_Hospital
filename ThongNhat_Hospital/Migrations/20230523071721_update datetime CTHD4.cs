using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThongNhat_Hospital.Migrations
{
    public partial class updatedatetimeCTHD4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Thoigian",
                table: "ChiTietDonHang",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thoigian",
                table: "ChiTietDonHang");
        }
    }
}
