using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreReferencesSample.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John" },
                    { 2, "Mike" },
                    { 3, "Ron" },
                    { 4, "Jennifer" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Scooby", null },
                    { 2, "Lessi", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
