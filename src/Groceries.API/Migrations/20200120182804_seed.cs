using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Groceries.API.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groceries",
                columns: table => new
                {
                    Fruit = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    QuantityInStock = table.Column<int>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groceries", x => x.Fruit);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groceries");
        }
    }
}
