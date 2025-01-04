using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apoint_pro.Migrations
{
    /// <inheritdoc />
    public partial class addDefultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "password",
                value: "fOIhAkxNOX07jSP7UBgNJ3b/4rB6Jk3JYO3nOt4wMN85RbUd");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "password",
                value: "SEFIg7uq/jLNXapXYrWFWwy/ge9nZJBjHfMMH6SCQB7VGgPg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "password",
                value: "XgWlMnf3Fnz9zLef914Ntb6AdeuQe8fB8IEFSa0zCjk1Hczf");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "password",
                value: "Dqy2jPp8AndovDJUtGLiAL2UHLMWzcNS89xBC5ScTTawtFZm");
        }
    }
}
