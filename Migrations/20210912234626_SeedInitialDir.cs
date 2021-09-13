using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_cloudstorage.Migrations
{
    public partial class SeedInitialDir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LtlDirectories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentDirId", "UniqueId", "UserInfoId" },
                values: new object[] { 1, new DateTime(2021, 9, 12, 16, 46, 25, 619, DateTimeKind.Local).AddTicks(5244), "Default", null, "13c44ee6-0628-4cfc-9690-5c6bfb357df6", 1 });

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 16, 46, 25, 619, DateTimeKind.Local).AddTicks(2375), new DateTime(2021, 9, 12, 16, 46, 25, 619, DateTimeKind.Local).AddTicks(3384) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastLoginAt" },
                values: new object[] { new DateTime(2021, 9, 12, 16, 46, 25, 619, DateTimeKind.Local).AddTicks(1058), new DateTime(2021, 9, 12, 16, 46, 25, 619, DateTimeKind.Local).AddTicks(1077) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LtlDirectories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 9, 12, 15, 2, 31, 334, DateTimeKind.Local).AddTicks(8515), new DateTime(2021, 9, 12, 15, 2, 31, 334, DateTimeKind.Local).AddTicks(9649) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastLoginAt" },
                values: new object[] { new DateTime(2021, 9, 12, 15, 2, 31, 334, DateTimeKind.Local).AddTicks(1111), new DateTime(2021, 9, 12, 15, 2, 31, 334, DateTimeKind.Local).AddTicks(1127) });
        }
    }
}
