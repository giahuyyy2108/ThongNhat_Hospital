using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_Hospital.Migrations
{
    public partial class adddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhieuGiaoHangID",
                table: "PhieuGiaoHang",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HinhThuc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHang",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id_LoaiHang = table.Column<string>(type: "varchar(400)", nullable: true),
                    Id_HinhThuc = table.Column<string>(type: "varchar(400)", nullable: true),
                    Id_PhieuGiao = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_HinhThuc_Id_HinhThuc",
                        column: x => x.Id_HinhThuc,
                        principalTable: "HinhThuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_LoaiHang_Id_LoaiHang",
                        column: x => x.Id_LoaiHang,
                        principalTable: "LoaiHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_PhieuGiaoHang_Id_PhieuGiao",
                        column: x => x.Id_PhieuGiao,
                        principalTable: "PhieuGiaoHang",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_Users_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang",
                column: "PhieuGiaoHangID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_Id_HinhThuc",
                table: "ChiTietDonHang",
                column: "Id_HinhThuc");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_Id_LoaiHang",
                table: "ChiTietDonHang",
                column: "Id_LoaiHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_Id_PhieuGiao",
                table: "ChiTietDonHang",
                column: "Id_PhieuGiao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_Id_User",
                table: "ChiTietDonHang",
                column: "Id_User");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuGiaoHang_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang",
                column: "PhieuGiaoHangID",
                principalTable: "PhieuGiaoHang",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuGiaoHang_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "HinhThuc");

            migrationBuilder.DropTable(
                name: "LoaiHang");

            migrationBuilder.DropIndex(
                name: "IX_PhieuGiaoHang_PhieuGiaoHangID",
                table: "PhieuGiaoHang");

            migrationBuilder.DropColumn(
                name: "PhieuGiaoHangID",
                table: "PhieuGiaoHang");
        }
    }
}
