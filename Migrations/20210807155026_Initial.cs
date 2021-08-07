using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ltl_cloudstorage.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    MembershipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Reputation = table.Column<int>(type: "int", nullable: false),
                    Introduction = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LtlDirectories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LtlDirectories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LtlDirectories_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LtlFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    DirectoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LtlFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LtlFiles_LtlDirectories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "LtlDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Color", "Description", "Name", "Price" },
                values: new object[] { 1, "linear-gradient(90deg, rgb(195, 88, 43) 0%, rgb(255, 182, 117) 50%, rgb(195, 88, 43) 100%)", "# Bronze", "Bronze", 10.0 });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Color", "Description", "Name", "Price" },
                values: new object[] { 2, "linear-gradient(90deg, rgba(151,150,149,1) 0%, rgba(210,210,210,1) 50%, rgba(151,150,149,1) 100%)", "", "Silver", 15.0 });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Color", "Description", "Name", "Price" },
                values: new object[] { 3, "linear-gradient(90deg, rgba(249,194,56,1) 0%, rgba(255,236,188,1) 50%, rgba(249,194,56,1) 100%)", "", "Gold", 20.0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CreatedAt", "DisplayName", "Email", "LastLoginAt", "MembershipId", "Name", "PasswordHash" },
                values: new object[] { 1, null, new DateTime(2021, 7, 26, 23, 0, 48, 0, DateTimeKind.Unspecified), "ltl", "tielinli@yahoo.com", new DateTime(2021, 7, 27, 8, 19, 4, 0, DateTimeKind.Unspecified), 2, "ltl", "oIn5JKeGBFsnpRAekK4jTQ==" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CreatedAt", "DisplayName", "Email", "LastLoginAt", "MembershipId", "Name", "PasswordHash" },
                values: new object[] { 2, null, new DateTime(2021, 7, 27, 7, 11, 41, 0, DateTimeKind.Unspecified), "LisaLee", "1248988727@qq.com", new DateTime(2021, 7, 27, 7, 11, 41, 0, DateTimeKind.Unspecified), 2, "LisaLee", "eMoP6zKEDM9eDMEYtFm4VA==" });

            migrationBuilder.CreateIndex(
                name: "IX_LtlDirectories_UserInfoId",
                table: "LtlDirectories",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LtlFiles_DirectoryId",
                table: "LtlFiles",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MembershipId",
                table: "Users",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LtlFiles");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "LtlDirectories");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Memberships");
        }
    }
}
