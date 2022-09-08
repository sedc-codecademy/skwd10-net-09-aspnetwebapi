using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.WebApi.Class06.CodeFirst.Migrations
{
    public partial class ADD_DATA_USER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "City", "Email", "Name" },
                values: new object[] { 1, "Temnica", "Skopje", "a@b.com", "Trajan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
