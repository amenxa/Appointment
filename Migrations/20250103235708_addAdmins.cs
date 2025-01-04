using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Apoint_pro.Migrations
{
    /// <inheritdoc />
    public partial class addAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "UserType", "password", "phone" },
                values: new object[,]
                {
                    { 1, "Ameen@prime.com", "Ameen", 1, "XgWlMnf3Fnz9zLef914Ntb6AdeuQe8fB8IEFSa0zCjk1Hczf", null },
                    { 2, "Rawan@prime.com", "Rawan", 1, "Dqy2jPp8AndovDJUtGLiAL2UHLMWzcNS89xBC5ScTTawtFZm", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
