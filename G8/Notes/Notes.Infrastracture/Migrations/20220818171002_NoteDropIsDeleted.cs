using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Infrastracture.Migrations
{
    public partial class NoteDropIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.Notes where IsDeleted = 1");
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
