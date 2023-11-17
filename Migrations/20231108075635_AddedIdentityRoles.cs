using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelHosting.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35d67924-0e26-434a-93d8-7a9c20e0e70d", null, "User", "USER" },
                    { "f8fe9368-00e8-455e-9d1c-aac4ac2e06d1", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35d67924-0e26-434a-93d8-7a9c20e0e70d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8fe9368-00e8-455e-9d1c-aac4ac2e06d1");
        }
    }
}
