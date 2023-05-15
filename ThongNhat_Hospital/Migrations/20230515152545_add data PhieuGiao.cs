using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_Hospital.Migrations
{
    public partial class adddataPhieuGiao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuGiaoHang_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang");

            migrationBuilder.DropIndex(
                name: "IX_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang");

            migrationBuilder.DropColumn(
                name: "PhieuGiaoHangID",
                table: "PhieuGiaoHang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhieuGiaoHangID",
                table: "PhieuGiaoHang",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang",
                column: "PhieuGiaoHangID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuGiaoHang_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang",
                column: "PhieuGiaoHangID",
                principalTable: "PhieuGiaoHang",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
