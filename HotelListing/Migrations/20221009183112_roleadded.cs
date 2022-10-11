using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Migrations
{
    public partial class roleadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1aef6dfc-b174-48ab-bbb8-3dbd9a7b34b1", "8610097c-bbd2-470a-ba82-8250d2609f5d", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ae3d1f5-b644-48b8-a5e0-68aa79cb248f", "aa315e13-f241-44b0-bd26-09230e7651f4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aef6dfc-b174-48ab-bbb8-3dbd9a7b34b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ae3d1f5-b644-48b8-a5e0-68aa79cb248f");
        }
    }
}
