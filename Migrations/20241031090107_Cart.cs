using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3aee472-959b-4cca-9000-bddb1258c229");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7660c63-5333-4c49-a3c4-be3c6c32716c");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUsersId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => new { x.MovieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Cards_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ed19684-9577-485b-9a83-e54907b8873f", "d6d015e1-3ff4-4352-b5b9-7d6b01230893", "User", "user" },
                    { "ec6cf92f-1dc5-4b37-9cfb-c682a95295e1", "429dc771-8564-4baf-9bbf-459431a22efe", "Admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ApplicationUsersId",
                table: "Cards",
                column: "ApplicationUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ed19684-9577-485b-9a83-e54907b8873f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec6cf92f-1dc5-4b37-9cfb-c682a95295e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3aee472-959b-4cca-9000-bddb1258c229", "426bec7e-258e-4c31-bfb2-893407e7f308", "Admin", "admin" },
                    { "e7660c63-5333-4c49-a3c4-be3c6c32716c", "9506455a-a5d4-445f-b054-63632359f950", "User", "user" }
                });
        }
    }
}
