using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class Count : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d1e6acb-e4a2-4dd5-a14a-7d69480b81f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b0f4e35-db7a-4ea1-8900-14d911db8a4a");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TicketsCount",
                table: "Cinemas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a50c22f5-a801-4a4a-94b6-9387a592eaa6", "a535f196-882b-4df5-84fe-64fb3b2f4391", "User", "user" },
                    { "d7723a0a-6f85-410a-8830-8124561696d9", "abae26ec-2a0c-4444-8dce-bbfaa89887c0", "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a50c22f5-a801-4a4a-94b6-9387a592eaa6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7723a0a-6f85-410a-8830-8124561696d9");

            migrationBuilder.DropColumn(
                name: "TicketsCount",
                table: "Cinemas");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d1e6acb-e4a2-4dd5-a14a-7d69480b81f8", "6738f682-470b-44c8-9c9c-e05c2ce849fa", "Admin", "admin" },
                    { "5b0f4e35-db7a-4ea1-8900-14d911db8a4a", "08d35330-396d-4f23-8e6e-7770e7d6dfca", "User", "user" }
                });
        }
    }
}
