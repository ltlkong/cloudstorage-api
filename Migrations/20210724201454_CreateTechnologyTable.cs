using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ltl_pf.Migrations
{
    public partial class CreateTechnologyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserKnowTechnology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserInfoId = table.Column<int>(type: "int", nullable: true),
                    TechnologyId = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKnowTechnology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserKnowTechnology_Technology_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technology",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserKnowTechnology_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserKnowTechnology_TechnologyId",
                table: "UserKnowTechnology",
                column: "TechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKnowTechnology_UserInfoId",
                table: "UserKnowTechnology",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserKnowTechnology");

            migrationBuilder.DropTable(
                name: "Technology");
        }
    }
}
