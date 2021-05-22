using Microsoft.EntityFrameworkCore.Migrations;

namespace Circular.Migrations
{
    public partial class addproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Project",
                newName: "language");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Project",
                newName: "createDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "language",
                table: "Project",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "createDate",
                table: "Project",
                newName: "dateTime");
        }
    }
}
