using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_Hospital.Migrations
{
    public partial class datavv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiTietHang",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    Id_PhieuGiao = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHang", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChiTietHang_PhieuGiaoHang_Id_PhieuGiao",
                        column: x => x.Id_PhieuGiao,
                        principalTable: "PhieuGiaoHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHang_Id_PhieuGiao",
                table: "ChiTietHang",
                column: "Id_PhieuGiao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHang");

           
        }
    }
}
