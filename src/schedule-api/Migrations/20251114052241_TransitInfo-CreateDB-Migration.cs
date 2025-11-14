using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schedule_api.Migrations
{
    /// <inheritdoc />
    public partial class TransitInfoCreateDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    StopId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.StopId);
                });

            migrationBuilder.CreateTable(
                name: "TopLevelRoutes",
                columns: table => new
                {
                    TopLevelRouteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TopLevelRouteName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopLevelRoutes", x => x.TopLevelRouteId);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TopLevelRouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Start = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    End = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Frequency = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ScheduleDay = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK_Routes_TopLevelRoutes_TopLevelRouteId",
                        column: x => x.TopLevelRouteId,
                        principalTable: "TopLevelRoutes",
                        principalColumn: "TopLevelRouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteSchedules",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    StopId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleOffset = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteSchedules", x => new { x.RouteId, x.StopId });
                    table.ForeignKey(
                        name: "FK_RouteSchedules_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteSchedules_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "StopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Stops",
                columns: new[] { "StopId", "Address" },
                values: new object[,]
                {
                    { 1, "Beaudry Ave & 3rd St" },
                    { 2, "4th St & Figueroa St" },
                    { 3, "Flower St & 5th St" },
                    { 4, "Flower St & 7th St" },
                    { 5, "Flower St & 8th St" },
                    { 6, "Flower St & 9th St" },
                    { 7, "Flower St & Olympic Blvd" },
                    { 8, "Figueroa & 12th St (Crypto.com Arena)" },
                    { 9, "Figueroa St & Pico Blvd" },
                    { 10, "Figueroa St & Venice Blvd" },
                    { 11, "Figueroa St & Washington Blvd" },
                    { 12, "Figueroa St & 23rd St" },
                    { 13, "Figueroa St & Adams Blvd" },
                    { 14, "Figueroa St & 30th St" },
                    { 15, "Figueroa St & Jefferson Blvd" },
                    { 16, "Figueroa St & McCarthy Way" },
                    { 17, "Exposition Blvd & Trousdale Pkwy" },
                    { 18, "Exposition Blvd & Watt Way" }
                });

            migrationBuilder.InsertData(
                table: "TopLevelRoutes",
                columns: new[] { "TopLevelRouteId", "TopLevelRouteName" },
                values: new object[,]
                {
                    { 1, "DASH Downtown F Northbound" },
                    { 2, "DASH Downtown F Southbound" }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "RouteId", "End", "Frequency", "ScheduleDay", "Start", "TopLevelRouteId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 0, 10, 0, 0), 0, new TimeSpan(0, 6, 0, 0, 0), 1 },
                    { 2, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 0, 15, 0, 0), 1, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 3, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 0, 15, 0, 0), 2, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 4, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 0, 10, 0, 0), 0, new TimeSpan(0, 6, 0, 0, 0), 2 },
                    { 5, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 0, 15, 0, 0), 1, new TimeSpan(0, 9, 0, 0, 0), 2 },
                    { 6, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 0, 15, 0, 0), 2, new TimeSpan(0, 9, 0, 0, 0), 2 }
                });

            migrationBuilder.InsertData(
                table: "RouteSchedules",
                columns: new[] { "RouteId", "StopId", "ScheduleOffset" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 0, 0, 0, 0) },
                    { 1, 2, new TimeSpan(0, 0, 3, 0, 0) },
                    { 1, 3, new TimeSpan(0, 0, 6, 0, 0) },
                    { 1, 4, new TimeSpan(0, 0, 9, 0, 0) },
                    { 1, 5, new TimeSpan(0, 0, 12, 0, 0) },
                    { 1, 6, new TimeSpan(0, 0, 15, 0, 0) },
                    { 1, 7, new TimeSpan(0, 0, 18, 0, 0) },
                    { 1, 8, new TimeSpan(0, 0, 21, 0, 0) },
                    { 1, 9, new TimeSpan(0, 0, 24, 0, 0) },
                    { 1, 10, new TimeSpan(0, 0, 27, 0, 0) },
                    { 1, 11, new TimeSpan(0, 0, 30, 0, 0) },
                    { 1, 12, new TimeSpan(0, 0, 33, 0, 0) },
                    { 1, 13, new TimeSpan(0, 0, 36, 0, 0) },
                    { 1, 14, new TimeSpan(0, 0, 39, 0, 0) },
                    { 1, 15, new TimeSpan(0, 0, 42, 0, 0) },
                    { 1, 16, new TimeSpan(0, 0, 45, 0, 0) },
                    { 1, 17, new TimeSpan(0, 0, 48, 0, 0) },
                    { 1, 18, new TimeSpan(0, 0, 51, 0, 0) },
                    { 2, 1, new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, 2, new TimeSpan(0, 0, 3, 0, 0) },
                    { 2, 3, new TimeSpan(0, 0, 6, 0, 0) },
                    { 2, 4, new TimeSpan(0, 0, 9, 0, 0) },
                    { 2, 5, new TimeSpan(0, 0, 12, 0, 0) },
                    { 2, 6, new TimeSpan(0, 0, 15, 0, 0) },
                    { 2, 7, new TimeSpan(0, 0, 18, 0, 0) },
                    { 2, 8, new TimeSpan(0, 0, 21, 0, 0) },
                    { 2, 9, new TimeSpan(0, 0, 24, 0, 0) },
                    { 2, 10, new TimeSpan(0, 0, 27, 0, 0) },
                    { 2, 11, new TimeSpan(0, 0, 30, 0, 0) },
                    { 2, 12, new TimeSpan(0, 0, 33, 0, 0) },
                    { 2, 13, new TimeSpan(0, 0, 36, 0, 0) },
                    { 2, 14, new TimeSpan(0, 0, 39, 0, 0) },
                    { 2, 15, new TimeSpan(0, 0, 42, 0, 0) },
                    { 2, 16, new TimeSpan(0, 0, 45, 0, 0) },
                    { 2, 17, new TimeSpan(0, 0, 48, 0, 0) },
                    { 2, 18, new TimeSpan(0, 0, 51, 0, 0) },
                    { 3, 1, new TimeSpan(0, 0, 0, 0, 0) },
                    { 3, 2, new TimeSpan(0, 0, 3, 0, 0) },
                    { 3, 3, new TimeSpan(0, 0, 6, 0, 0) },
                    { 3, 4, new TimeSpan(0, 0, 9, 0, 0) },
                    { 3, 5, new TimeSpan(0, 0, 12, 0, 0) },
                    { 3, 6, new TimeSpan(0, 0, 15, 0, 0) },
                    { 3, 7, new TimeSpan(0, 0, 18, 0, 0) },
                    { 3, 8, new TimeSpan(0, 0, 21, 0, 0) },
                    { 3, 9, new TimeSpan(0, 0, 24, 0, 0) },
                    { 3, 10, new TimeSpan(0, 0, 27, 0, 0) },
                    { 3, 11, new TimeSpan(0, 0, 30, 0, 0) },
                    { 3, 12, new TimeSpan(0, 0, 33, 0, 0) },
                    { 3, 13, new TimeSpan(0, 0, 36, 0, 0) },
                    { 3, 14, new TimeSpan(0, 0, 39, 0, 0) },
                    { 3, 15, new TimeSpan(0, 0, 42, 0, 0) },
                    { 3, 16, new TimeSpan(0, 0, 45, 0, 0) },
                    { 3, 17, new TimeSpan(0, 0, 48, 0, 0) },
                    { 3, 18, new TimeSpan(0, 0, 51, 0, 0) },
                    { 4, 1, new TimeSpan(0, 0, 51, 0, 0) },
                    { 4, 2, new TimeSpan(0, 0, 48, 0, 0) },
                    { 4, 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 4, 4, new TimeSpan(0, 0, 42, 0, 0) },
                    { 4, 5, new TimeSpan(0, 0, 39, 0, 0) },
                    { 4, 6, new TimeSpan(0, 0, 36, 0, 0) },
                    { 4, 7, new TimeSpan(0, 0, 33, 0, 0) },
                    { 4, 8, new TimeSpan(0, 0, 30, 0, 0) },
                    { 4, 9, new TimeSpan(0, 0, 27, 0, 0) },
                    { 4, 10, new TimeSpan(0, 0, 24, 0, 0) },
                    { 4, 11, new TimeSpan(0, 0, 21, 0, 0) },
                    { 4, 12, new TimeSpan(0, 0, 18, 0, 0) },
                    { 4, 13, new TimeSpan(0, 0, 15, 0, 0) },
                    { 4, 14, new TimeSpan(0, 0, 12, 0, 0) },
                    { 4, 15, new TimeSpan(0, 0, 9, 0, 0) },
                    { 4, 16, new TimeSpan(0, 0, 6, 0, 0) },
                    { 4, 17, new TimeSpan(0, 0, 3, 0, 0) },
                    { 4, 18, new TimeSpan(0, 0, 0, 0, 0) },
                    { 5, 1, new TimeSpan(0, 0, 51, 0, 0) },
                    { 5, 2, new TimeSpan(0, 0, 48, 0, 0) },
                    { 5, 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 5, 4, new TimeSpan(0, 0, 42, 0, 0) },
                    { 5, 5, new TimeSpan(0, 0, 39, 0, 0) },
                    { 5, 6, new TimeSpan(0, 0, 36, 0, 0) },
                    { 5, 7, new TimeSpan(0, 0, 33, 0, 0) },
                    { 5, 8, new TimeSpan(0, 0, 30, 0, 0) },
                    { 5, 9, new TimeSpan(0, 0, 27, 0, 0) },
                    { 5, 10, new TimeSpan(0, 0, 24, 0, 0) },
                    { 5, 11, new TimeSpan(0, 0, 21, 0, 0) },
                    { 5, 12, new TimeSpan(0, 0, 18, 0, 0) },
                    { 5, 13, new TimeSpan(0, 0, 15, 0, 0) },
                    { 5, 14, new TimeSpan(0, 0, 12, 0, 0) },
                    { 5, 15, new TimeSpan(0, 0, 9, 0, 0) },
                    { 5, 16, new TimeSpan(0, 0, 6, 0, 0) },
                    { 5, 17, new TimeSpan(0, 0, 3, 0, 0) },
                    { 5, 18, new TimeSpan(0, 0, 0, 0, 0) },
                    { 6, 1, new TimeSpan(0, 0, 51, 0, 0) },
                    { 6, 2, new TimeSpan(0, 0, 48, 0, 0) },
                    { 6, 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 6, 4, new TimeSpan(0, 0, 42, 0, 0) },
                    { 6, 5, new TimeSpan(0, 0, 39, 0, 0) },
                    { 6, 6, new TimeSpan(0, 0, 36, 0, 0) },
                    { 6, 7, new TimeSpan(0, 0, 33, 0, 0) },
                    { 6, 8, new TimeSpan(0, 0, 30, 0, 0) },
                    { 6, 9, new TimeSpan(0, 0, 27, 0, 0) },
                    { 6, 10, new TimeSpan(0, 0, 24, 0, 0) },
                    { 6, 11, new TimeSpan(0, 0, 21, 0, 0) },
                    { 6, 12, new TimeSpan(0, 0, 18, 0, 0) },
                    { 6, 13, new TimeSpan(0, 0, 15, 0, 0) },
                    { 6, 14, new TimeSpan(0, 0, 12, 0, 0) },
                    { 6, 15, new TimeSpan(0, 0, 9, 0, 0) },
                    { 6, 16, new TimeSpan(0, 0, 6, 0, 0) },
                    { 6, 17, new TimeSpan(0, 0, 3, 0, 0) },
                    { 6, 18, new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TopLevelRouteId",
                table: "Routes",
                column: "TopLevelRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteSchedules_StopId",
                table: "RouteSchedules",
                column: "StopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteSchedules");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "TopLevelRoutes");
        }
    }
}
