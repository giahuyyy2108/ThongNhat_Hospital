using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ThongNhat_Hospital.Migrations
{
    public partial class UserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 0; i < 150; i++)
            {
                migrationBuilder.InsertData(
                    "Users",
                    columns: new[] {
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "img",
                        "PasswordHash"
                    },
                    values: new object[]
                    {
                        Guid.NewGuid().ToString(),
                        "user-"+i.ToString("D3"),
                        $"email{i.ToString()}@examble.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        false,
                        0,
                        "...@#%..",
                        "AQAAAAEAACcQAAAAEJb/+iKC2lQkYrQJc1/7kKHH0V4WjgwikJQBBgUO3hah20M+yU5BqtuBHtuAthOwDA=="
                    }

                    );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
