using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ltl_pf.Migrations
{
    public partial class CreateMembershipTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<byte[]>(type: "varbinary(4000)", nullable: true),
                    Description = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_MembershipId",
                table: "User",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Membership_MembershipId",
                table: "User",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Membership_MembershipId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropIndex(
                name: "IX_User_MembershipId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "User");
        }
    }
}
