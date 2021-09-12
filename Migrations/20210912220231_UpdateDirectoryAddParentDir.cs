using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_cloudstorage.Migrations
{
    public partial class UpdateDirectoryAddParentDir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentDirId",
                table: "LtlDirectories",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_LtlDirectories_ParentDirId",
                table: "LtlDirectories",
                column: "ParentDirId");

            migrationBuilder.AddForeignKey(
                name: "FK_LtlDirectories_LtlDirectories_ParentDirId",
                table: "LtlDirectories",
                column: "ParentDirId",
                principalTable: "LtlDirectories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LtlDirectories_LtlDirectories_ParentDirId",
                table: "LtlDirectories");

            migrationBuilder.DropIndex(
                name: "IX_LtlDirectories_ParentDirId",
                table: "LtlDirectories");

            migrationBuilder.DropColumn(
                name: "ParentDirId",
                table: "LtlDirectories");

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
    }
}
