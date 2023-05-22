using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThongNhat_Hospital.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                    "Users",
                    columns: new[]
                    {
                        "Id",
                        "hoten",
                        "UserName",
                        "Email",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "[PasswordHash]"
                    },
                    values: new object[]
                    {
                        Guid.NewGuid().ToString(),
                        "admin",
                        "Admin",
                        $"vuhuy110@gmail.com",
                        true,
                        false,
                        false,
                        false,
                        0,
                        "AQAAAAEAACcQAAAAEJd/ktBvLxEoGommJdbUX9yNe5pPocTsCzedd7biUf7HSD/l9mAWq4GFcpJKoc8D7g=="
                    }
            );

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
            migrationBuilder.InsertData(
                               "HinhThuc",
                               columns: new[]
                               {
                    "Id",
                    "Name"
                               },
                               values: new object[]
                               {
                    "1",
                    "Giao hàng"
                               }
                               );
            migrationBuilder.InsertData(
               "HinhThuc",
               columns: new[]
               {
                    "Id",
                    "Name"
               },
               values: new object[]
               {
                    "2",
                    "Nhận hàng"
               }
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
