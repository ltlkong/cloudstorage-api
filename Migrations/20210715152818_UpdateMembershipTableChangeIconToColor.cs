using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_pf.Migrations
{
    public partial class UpdateMembershipTableChangeIconToColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Membership_MembershipId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Membership");

            migrationBuilder.RenameTable(
                name: "Membership",
                newName: "Memberships");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Memberships",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Memberships_MembershipId",
                table: "User",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Memberships_MembershipId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Memberships");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "Membership");

            migrationBuilder.AddColumn<byte[]>(
                name: "Icon",
                table: "Membership",
                type: "varbinary(4000)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Membership_MembershipId",
                table: "User",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
