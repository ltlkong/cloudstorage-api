using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_cloudstorage.Migrations
{
    public partial class UpdateFileAddIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Memberships",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "LtlFiles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 13, 55, 5, 665, DateTimeKind.Local).AddTicks(2596), new DateTime(2021, 9, 12, 13, 55, 5, 665, DateTimeKind.Local).AddTicks(3735) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastLoginAt" },
                values: new object[] { new DateTime(2021, 9, 12, 13, 55, 5, 664, DateTimeKind.Local).AddTicks(4797), new DateTime(2021, 9, 12, 13, 55, 5, 664, DateTimeKind.Local).AddTicks(4815) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "LtlFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Memberships",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 8, 8, 23, 34, 24, 355, DateTimeKind.Local).AddTicks(9000), new DateTime(2021, 8, 8, 23, 34, 24, 356, DateTimeKind.Local).AddTicks(5799) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastLoginAt" },
                values: new object[] { new DateTime(2021, 8, 8, 23, 34, 24, 342, DateTimeKind.Local).AddTicks(995), new DateTime(2021, 8, 8, 23, 34, 24, 342, DateTimeKind.Local).AddTicks(1195) });
        }
    }
}
