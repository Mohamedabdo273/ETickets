using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class Trigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "378c8aa5-6a43-45b6-84ae-f81b95b00c71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd3c76a8-c78f-48fe-a1fb-ea88834415d1");

            migrationBuilder.CreateTable(
                name: "Movie_audit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie_audit", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5af6a0c4-ba35-4359-8747-9dab9a12e9f8", "a2206017-a470-4a35-870e-01e58c6a9a65", "User", "user" },
                    { "a7e7c93f-9649-4e8d-8c54-83063122b58c", "fb29e39e-185f-42da-b5b8-b3aecf846bb9", "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie_audit");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5af6a0c4-ba35-4359-8747-9dab9a12e9f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7e7c93f-9649-4e8d-8c54-83063122b58c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "378c8aa5-6a43-45b6-84ae-f81b95b00c71", "82088daa-d56f-42c2-8323-cdc8006d32f6", "Admin", "admin" },
                    { "cd3c76a8-c78f-48fe-a1fb-ea88834415d1", "043c6533-8c8b-46d1-b56a-62e0e3b69eeb", "User", "user" }
                });
        }
    }
}
