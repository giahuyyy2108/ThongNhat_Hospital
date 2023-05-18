using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_Hospital.Migrations
{
    public partial class addHoten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hoten",
                table: "Users",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hoten",
                table: "Users");
        }
    }
}
