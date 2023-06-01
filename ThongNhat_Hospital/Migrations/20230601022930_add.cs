using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThongNhat_Hospital.Migrations
{
    public partial class add : Migration
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
                        "SecurityStamp"
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
                        0,
                        Guid.NewGuid().ToString(),
                    }
                    );

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
