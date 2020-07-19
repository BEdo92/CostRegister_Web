using Microsoft.EntityFrameworkCore.Migrations;

namespace CostRegApp2.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostPlans_Users_UserId",
                table: "CostPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Users_UserId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Users_UserId",
                table: "Income");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Income",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Costs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CostPlans",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CostPlans_Users_UserId",
                table: "CostPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Users_UserId",
                table: "Costs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Users_UserId",
                table: "Income",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostPlans_Users_UserId",
                table: "CostPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Users_UserId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Users_UserId",
                table: "Income");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Income",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Costs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CostPlans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CostPlans_Users_UserId",
                table: "CostPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Users_UserId",
                table: "Costs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Users_UserId",
                table: "Income",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
