using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThongNhat_Hospital.Migrations
{
    public partial class adddata : Migration
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
                        "SecurityStamp"
                    },
                    values: new object[]
                    {
                        Guid.NewGuid().ToString(),
                        "Admin",
                        "admin",
                        "vuhuy110@gmail.com",
                        true,
                        false,
                        false,
                        false,
                        0,
                        Guid.NewGuid().ToString(),
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
