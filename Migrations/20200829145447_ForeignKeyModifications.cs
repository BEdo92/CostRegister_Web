using Microsoft.EntityFrameworkCore.Migrations;

namespace CostRegApp2.Migrations
{
    public partial class ForeignKeyModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostPlans_Categories_CategoryID",
                table: "CostPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Categories_CategoryID",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Shops_ShopID",
                table: "Costs");

            migrationBuilder.AddForeignKey(
                name: "FK_CostPlans_Categories_CategoryID",
                table: "CostPlans",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Categories_CategoryID",
                table: "Costs",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Shops_ShopID",
                table: "Costs",
                column: "ShopID",
                principalTable: "Shops",
                principalColumn: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostPlans_Categories_CategoryID",
                table: "CostPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Categories_CategoryID",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Shops_ShopID",
                table: "Costs");

            migrationBuilder.AddForeignKey(
                name: "FK_CostPlans_Categories_CategoryID",
                table: "CostPlans",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Categories_CategoryID",
                table: "Costs",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Shops_ShopID",
                table: "Costs",
                column: "ShopID",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
