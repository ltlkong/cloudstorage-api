using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_pf.Migrations
{
    public partial class UpdateUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(4000)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "User",
                type: "varbinary(4000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
