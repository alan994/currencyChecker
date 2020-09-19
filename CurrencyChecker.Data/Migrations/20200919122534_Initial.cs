using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyChecker.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Mark = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    LastChanged = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Buy = table.Column<decimal>(nullable: false),
                    Middle = table.Column<decimal>(nullable: false),
                    Sell = table.Column<decimal>(nullable: false),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
