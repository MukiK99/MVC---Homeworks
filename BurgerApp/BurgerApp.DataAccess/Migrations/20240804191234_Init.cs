using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerApp.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Burger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    HasFries = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpensAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosesAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBurgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BurgerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBurgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderBurgers_Burger_BurgerId",
                        column: x => x.BurgerId,
                        principalTable: "Burger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBurgers_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Burger",
                columns: new[] { "Id", "HasFries", "IsVegan", "IsVegetarian", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, false, false, "Hamburger", 2.9900000000000002 },
                    { 2, true, false, false, "Cheeseburger", 3.7000000000000002 },
                    { 3, true, false, false, "Chickenburger", 3.7999999999999998 },
                    { 4, false, false, true, "Veggieburger", 2.7000000000000002 },
                    { 5, false, true, true, "Veganburger", 1.8999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "Address", "ClosesAt", "Name", "OpensAt" },
                values: new object[,]
                {
                    { 1, "ul.Makedonija", new DateTime(2024, 8, 4, 22, 0, 0, 0, DateTimeKind.Local), "MacShop", new DateTime(2024, 8, 4, 8, 0, 0, 0, DateTimeKind.Local) },
                    { 2, "Chinatown", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), "ChinaShop", new DateTime(2024, 8, 4, 8, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Address", "FullName", "IsDelivered", "LocationId" },
                values: new object[,]
                {
                    { 1, "Partizanski Odredi BB", "Murat Koca", true, 1 },
                    { 2, "Ilindenska BB", "Radica Shvigir", true, 1 },
                    { 3, "Guangzhou 11", "Jack Man", false, 1 },
                    { 4, "Manhattan 23", "John Legends", false, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderBurgers",
                columns: new[] { "Id", "BurgerId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 100, 3, 1, 2 },
                    { 101, 4, 1, 4 },
                    { 102, 1, 2, 3 },
                    { 103, 2, 3, 3 },
                    { 104, 3, 3, 6 },
                    { 105, 5, 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_LocationId",
                table: "Order",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBurgers_BurgerId",
                table: "OrderBurgers",
                column: "BurgerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBurgers_OrderId",
                table: "OrderBurgers",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBurgers");

            migrationBuilder.DropTable(
                name: "Burger");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
