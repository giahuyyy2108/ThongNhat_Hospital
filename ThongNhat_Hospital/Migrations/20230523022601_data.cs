using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThongNhat_Hospital.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 0; i < 150; i++)
            {
                migrationBuilder.InsertData(
                    "Users",
                    columns: new[]
                    {
                        "Id",
                        "img",
                        "hoten",
                        "UserName",
                        "Email",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                    },
                    values: new object[]
                    {
                        Guid.NewGuid().ToString(),
                        "...@!...",
                        "user-"+i.ToString("D3"),
                        "user-"+i.ToString("D3"),
                        $"user-{i.ToString()}@example.com",
                        true,
                        false,
                        false,
                        false,
                        0
                    }
                    );
                
            }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
