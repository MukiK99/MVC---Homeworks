using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerApp.DataAccess.Migrations
{
    public partial class AddedCompositePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBurgers",
                table: "OrderBurgers");

            migrationBuilder.DropIndex(
                name: "IX_OrderBurgers_OrderId",
                table: "OrderBurgers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBurgers",
                table: "OrderBurgers",
                columns: new[] { "OrderId", "BurgerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBurgers",
                table: "OrderBurgers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBurgers",
                table: "OrderBurgers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBurgers_OrderId",
                table: "OrderBurgers",
                column: "OrderId");
        }
    }
}
