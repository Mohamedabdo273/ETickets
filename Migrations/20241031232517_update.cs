using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5af6a0c4-ba35-4359-8747-9dab9a12e9f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7e7c93f-9649-4e8d-8c54-83063122b58c");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Movie_audit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "Movie_audit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "operation",
                table: "Movie_audit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d1e6acb-e4a2-4dd5-a14a-7d69480b81f8", "6738f682-470b-44c8-9c9c-e05c2ce849fa", "Admin", "admin" },
                    { "5b0f4e35-db7a-4ea1-8900-14d911db8a4a", "08d35330-396d-4f23-8e6e-7770e7d6dfca", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d1e6acb-e4a2-4dd5-a14a-7d69480b81f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b0f4e35-db7a-4ea1-8900-14d911db8a4a");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Movie_audit");

            migrationBuilder.DropColumn(
                name: "count",
                table: "Movie_audit");

            migrationBuilder.DropColumn(
                name: "operation",
                table: "Movie_audit");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5af6a0c4-ba35-4359-8747-9dab9a12e9f8", "a2206017-a470-4a35-870e-01e58c6a9a65", "User", "user" },
                    { "a7e7c93f-9649-4e8d-8c54-83063122b58c", "fb29e39e-185f-42da-b5b8-b3aecf846bb9", "Admin", "admin" }
                });
        }
    }
}
