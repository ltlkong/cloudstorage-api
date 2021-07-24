using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_pf.Migrations
{
    public partial class UpdateTechnologyTableAddedIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Technology",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Technology",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technology_Name",
                table: "Technology",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Technology_Name",
                table: "Technology");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Technology");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Technology",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
