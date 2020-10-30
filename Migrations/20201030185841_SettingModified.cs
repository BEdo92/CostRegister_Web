using Microsoft.EntityFrameworkCore.Migrations;

namespace CostRegApp2.Migrations
{
    public partial class SettingModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Setting",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Setting");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "UserSetting",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "UserSetting");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Setting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "IncludePlansInBalance", "true" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 2, "IncludePlansInBalance", "false" });
        }
    }
}
