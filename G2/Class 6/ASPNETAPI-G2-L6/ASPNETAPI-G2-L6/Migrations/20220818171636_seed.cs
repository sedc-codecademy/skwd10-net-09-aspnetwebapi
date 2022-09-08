using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNETAPI_G2_L6.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { -2, "Bojan", "Damcevski", "sedc456", "bokid123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { -1, "Mihajlo", "Dimovski", "sedc456", "mikid123" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Color", "Tag", "Text", "UserId" },
                values: new object[,]
                {
                    { -4, "Yellow", 5, "Text Office 4444", -2 },
                    { -3, "Yellow", 5, "Text Office 4444", -1 },
                    { -2, "Green", 3, "Text 234", -1 },
                    { -1, "Red", 1, "Text 123", -1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
