using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class quantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ed19684-9577-485b-9a83-e54907b8873f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec6cf92f-1dc5-4b37-9cfb-c682a95295e1");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "378c8aa5-6a43-45b6-84ae-f81b95b00c71", "82088daa-d56f-42c2-8323-cdc8006d32f6", "Admin", "admin" },
                    { "cd3c76a8-c78f-48fe-a1fb-ea88834415d1", "043c6533-8c8b-46d1-b56a-62e0e3b69eeb", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "378c8aa5-6a43-45b6-84ae-f81b95b00c71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd3c76a8-c78f-48fe-a1fb-ea88834415d1");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ed19684-9577-485b-9a83-e54907b8873f", "d6d015e1-3ff4-4352-b5b9-7d6b01230893", "User", "user" },
                    { "ec6cf92f-1dc5-4b37-9cfb-c682a95295e1", "429dc771-8564-4baf-9bbf-459431a22efe", "Admin", "admin" }
                });
        }
    }
}
