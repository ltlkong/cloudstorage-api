using Microsoft.EntityFrameworkCore.Migrations;

namespace ltl_cloudstorage.Migrations
{
    public partial class UpdateDocumentTableAddTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Documents",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Documents");
        }
    }
}
