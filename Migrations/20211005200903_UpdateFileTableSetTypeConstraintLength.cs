using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_cloudstorage.Migrations
{
    public partial class UpdateFileTableSetTypeConstraintLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "LtlFiles",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LtlDirectories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 10, 5, 13, 9, 2, 606, DateTimeKind.Local).AddTicks(3954));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 5, 13, 9, 2, 606, DateTimeKind.Local).AddTicks(340), new DateTime(2021, 10, 5, 13, 9, 2, 606, DateTimeKind.Local).AddTicks(1465) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastLoginAt" },
                values: new object[] { new DateTime(2021, 10, 5, 13, 9, 2, 605, DateTimeKind.Local).AddTicks(9653), new DateTime(2021, 10, 5, 13, 9, 2, 605, DateTimeKind.Local).AddTicks(9673) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "LtlFiles",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LtlDirectories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 15, 9, 10, 33, 417, DateTimeKind.Local).AddTicks(2266));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 9, 15, 9, 10, 33, 416, DateTimeKind.Local).AddTicks(9105), new DateTime(2021, 9, 15, 9, 10, 33, 417, DateTimeKind.Local).AddTicks(148) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastLoginAt" },
                values: new object[] { new DateTime(2021, 9, 15, 9, 10, 33, 416, DateTimeKind.Local).AddTicks(8416), new DateTime(2021, 9, 15, 9, 10, 33, 416, DateTimeKind.Local).AddTicks(8433) });
        }
    }
}
