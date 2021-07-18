using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_pf.Migrations
{
    public partial class UpdateUserTableAddLastLoginDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "lastLoginAt",
                table: "User",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastLoginAt",
                table: "User");
        }
    }
}
