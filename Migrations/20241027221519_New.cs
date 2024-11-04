using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6147c63a-e874-4223-a561-f0da3dbb881a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af989eeb-cdf8-49f5-99dd-be472cdb4ee2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3aee472-959b-4cca-9000-bddb1258c229", "426bec7e-258e-4c31-bfb2-893407e7f308", "Admin", "admin" },
                    { "e7660c63-5333-4c49-a3c4-be3c6c32716c", "9506455a-a5d4-445f-b054-63632359f950", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3aee472-959b-4cca-9000-bddb1258c229");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7660c63-5333-4c49-a3c4-be3c6c32716c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6147c63a-e874-4223-a561-f0da3dbb881a", "d0db4d55-68c9-442a-81cf-af5e66801c6e", "User", "user" },
                    { "af989eeb-cdf8-49f5-99dd-be472cdb4ee2", "255ed463-a06a-4b2f-9400-f2e743baa072", "Admin", "admin" }
                });
        }
    }
}
