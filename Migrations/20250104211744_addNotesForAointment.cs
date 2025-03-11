using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apoint_pro.Migrations
{
    /// <inheritdoc />
    public partial class addNotesForAointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Apointments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "password",
                value: "ghiOclWXN94WVlLg3isWlyUNbMqSVv90PWmfDYTRoEAEuN6s");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "password",
                value: "9RwH4X+j2WLyOmOoN4RWiqUdvYSZaiE9/Q3oXUIU3tZK/oku");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Apointments");

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
    }
}
