using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_cloudstorage.Migrations
{
    public partial class UpdateUserTableNullableMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Memberships_MembershipId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "MembershipId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "LtlFiles",
                type: "varchar(767)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "LtlDirectories",
                type: "varchar(767)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "CreatedAt", "Introduction", "Reputation", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 8, 8, 23, 34, 24, 355, DateTimeKind.Local).AddTicks(9000), "# Public test user", 100, new DateTime(2021, 8, 8, 23, 34, 24, 356, DateTimeKind.Local).AddTicks(5799) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DisplayName", "Email", "LastLoginAt", "MembershipId", "Name", "PasswordHash" },
                values: new object[] { new DateTime(2021, 8, 8, 23, 34, 24, 342, DateTimeKind.Local).AddTicks(995), "public", "public@public.com", new DateTime(2021, 8, 8, 23, 34, 24, 342, DateTimeKind.Local).AddTicks(1195), 1, "public", "public" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DisplayName", "Email", "LastLoginAt", "Name", "PasswordHash" },
                values: new object[] { new DateTime(2021, 7, 26, 23, 0, 48, 0, DateTimeKind.Unspecified), "ltl", "tielinli@yahoo.com", new DateTime(2021, 7, 27, 8, 19, 4, 0, DateTimeKind.Unspecified), "ltl", "oIn5JKeGBFsnpRAekK4jTQ==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CreatedAt", "DisplayName", "Email", "LastLoginAt", "MembershipId", "Name", "PasswordHash" },
                values: new object[] { 3, null, new DateTime(2021, 7, 27, 7, 11, 41, 0, DateTimeKind.Unspecified), "LisaLee", "1248988727@qq.com", new DateTime(2021, 7, 27, 7, 11, 41, 0, DateTimeKind.Unspecified), 2, "LisaLee", "eMoP6zKEDM9eDMEYtFm4VA==" });

            migrationBuilder.CreateIndex(
                name: "IX_LtlFiles_UniqueId",
                table: "LtlFiles",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LtlDirectories_UniqueId",
                table: "LtlDirectories",
                column: "UniqueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Memberships_MembershipId",
                table: "Users",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Memberships_MembershipId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_LtlFiles_UniqueId",
                table: "LtlFiles");

            migrationBuilder.DropIndex(
                name: "IX_LtlDirectories_UniqueId",
                table: "LtlDirectories");

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "MembershipId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "LtlFiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "LtlDirectories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DisplayName", "Email", "LastLoginAt", "MembershipId", "Name", "PasswordHash" },
                values: new object[] { new DateTime(2021, 7, 26, 23, 0, 48, 0, DateTimeKind.Unspecified), "ltl", "tielinli@yahoo.com", new DateTime(2021, 7, 27, 8, 19, 4, 0, DateTimeKind.Unspecified), 2, "ltl", "oIn5JKeGBFsnpRAekK4jTQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DisplayName", "Email", "LastLoginAt", "Name", "PasswordHash" },
                values: new object[] { new DateTime(2021, 7, 27, 7, 11, 41, 0, DateTimeKind.Unspecified), "LisaLee", "1248988727@qq.com", new DateTime(2021, 7, 27, 7, 11, 41, 0, DateTimeKind.Unspecified), "LisaLee", "eMoP6zKEDM9eDMEYtFm4VA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Memberships_MembershipId",
                table: "Users",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
