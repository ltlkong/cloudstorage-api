using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ltl_webdev.Migrations
{
    public partial class UpdateUserInfoTableAlterRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserInfo_UserInfoId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserInfoId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_User_Id",
                table: "UserInfo",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_User_Id",
                table: "UserInfo");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserInfo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserInfoId",
                table: "User",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserInfo_UserInfoId",
                table: "User",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
