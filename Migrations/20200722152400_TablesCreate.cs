using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostRegApp2.Migrations
{
    public partial class TablesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CostPlans",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfCostPlan = table.Column<string>(maxLength: 100, nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    CostPlanned = table.Column<int>(nullable: false),
                    DateOfPlan = table.Column<DateTime>(nullable: false),
                    PlanAdditionalInformation = table.Column<string>(maxLength: 100, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostPlans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CostPlans_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Income",
                columns: table => new
                {
                    IncomeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfIncome = table.Column<string>(maxLength: 100, nullable: false),
                    AmountOfIncome = table.Column<int>(nullable: false),
                    DateOFIncome = table.Column<DateTime>(nullable: false),
                    IncomeAdditionalInformation = table.Column<string>(maxLength: 100, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income", x => x.IncomeID);
                    table.ForeignKey(
                        name: "FK_Income_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(nullable: false),
                    ShopID = table.Column<int>(nullable: false),
                    AmountOfCost = table.Column<int>(nullable: false),
                    DateOfCost = table.Column<DateTime>(nullable: false),
                    AdditionalInformation = table.Column<string>(maxLength: 100, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Costs_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Costs_Shops_ShopID",
                        column: x => x.ShopID,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Costs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostPlans_CategoryID",
                table: "CostPlans",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CostPlans_UserId",
                table: "CostPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CategoryID",
                table: "Costs",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ShopID",
                table: "Costs",
                column: "ShopID");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_UserId",
                table: "Costs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Income_UserId",
                table: "Income",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostPlans");

            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "Income");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }
    }
}
