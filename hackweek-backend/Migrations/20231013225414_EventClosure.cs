using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hackweek_backend.Migrations
{
    /// <inheritdoc />
    public partial class EventClosure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsPublic",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Qa4VR1oF4xejoq3sMif8BOj9Pr/68DFgYfkvvYwbxJGUpLvSwnHO2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$1sEfLMnfETCq022m32dBnur1Q4Wk9f.eHn8WvK8vTStN3GFArsBs6");
        }
    }
}
