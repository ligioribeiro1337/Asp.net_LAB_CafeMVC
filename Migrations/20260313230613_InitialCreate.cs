using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CafeChains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CuisineType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Regions = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Menu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FoundedYear = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeChains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Dishes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CafeChainId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CafeChains_CafeChainId",
                        column: x => x.CafeChainId,
                        principalTable: "CafeChains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CafeChains",
                columns: new[] { "Id", "CuisineType", "FoundedYear", "Menu", "Name", "Regions" },
                values: new object[,]
                {
                    { 1, "Фастфуд", 2022, "Бургеры, картофель фри", "Вкусно и точка", "Москва, Санкт-Петербург" },
                    { 2, "Русская", 1998, "Блины, каши, супы", "Теремок", "Москва" },
                    { 3, "Японская", 2003, "Суши, роллы, рамен", "Якитория", "Москва, Екатеринбург" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CafeChainId", "Dishes", "OrderNumber", "OrderTime", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Бургер классический, Кола", "ORD-001", new DateTime(2025, 1, 15, 12, 30, 0, 0, DateTimeKind.Utc), "Выполнен" },
                    { 2, 2, "Блин с мясом, Чай", "ORD-002", new DateTime(2025, 1, 16, 14, 0, 0, 0, DateTimeKind.Utc), "Готовится" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CafeChainId",
                table: "Orders",
                column: "CafeChainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CafeChains");
        }
    }
}
