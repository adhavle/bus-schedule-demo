using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace schedule_api.Migrations
{
    /// <inheritdoc />
    public partial class TransitInfoCreateDB : Migration
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
                name: "Schedule",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    StopId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScheduleOffset = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => new { x.RouteId, x.StopId });
                    table.ForeignKey(
                        name: "FK_Schedule_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Stops_StopId",
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
                    { 1, "Vermont Ave & Exposition Blvd" },
                    { 2, "Vermont Ave & 37th Pl" },
                    { 3, "Vermont Ave & 36th Pl" },
                    { 4, "Jefferson Blvd & Vermont Ave" },
                    { 5, "Jefferson Blvd & McClintock Ave" }
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
                table: "Schedule",
                columns: new[] { "RouteId", "StopId", "ScheduleOffset" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 0, 0, 0, 0) },
                    { 1, 2, new TimeSpan(0, 0, 5, 0, 0) },
                    { 1, 3, new TimeSpan(0, 0, 10, 0, 0) },
                    { 1, 4, new TimeSpan(0, 0, 15, 0, 0) },
                    { 1, 5, new TimeSpan(0, 0, 20, 0, 0) },
                    { 2, 1, new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, 2, new TimeSpan(0, 0, 5, 0, 0) },
                    { 2, 3, new TimeSpan(0, 0, 10, 0, 0) },
                    { 2, 4, new TimeSpan(0, 0, 15, 0, 0) },
                    { 2, 5, new TimeSpan(0, 0, 20, 0, 0) },
                    { 3, 1, new TimeSpan(0, 0, 0, 0, 0) },
                    { 3, 2, new TimeSpan(0, 0, 5, 0, 0) },
                    { 3, 3, new TimeSpan(0, 0, 10, 0, 0) },
                    { 3, 4, new TimeSpan(0, 0, 15, 0, 0) },
                    { 3, 5, new TimeSpan(0, 0, 20, 0, 0) },
                    { 4, 1, new TimeSpan(0, 0, 20, 0, 0) },
                    { 4, 2, new TimeSpan(0, 0, 15, 0, 0) },
                    { 4, 3, new TimeSpan(0, 0, 10, 0, 0) },
                    { 4, 4, new TimeSpan(0, 0, 5, 0, 0) },
                    { 4, 5, new TimeSpan(0, 0, 0, 0, 0) },
                    { 5, 1, new TimeSpan(0, 0, 20, 0, 0) },
                    { 5, 2, new TimeSpan(0, 0, 15, 0, 0) },
                    { 5, 3, new TimeSpan(0, 0, 10, 0, 0) },
                    { 5, 4, new TimeSpan(0, 0, 5, 0, 0) },
                    { 5, 5, new TimeSpan(0, 0, 0, 0, 0) },
                    { 6, 1, new TimeSpan(0, 0, 20, 0, 0) },
                    { 6, 2, new TimeSpan(0, 0, 15, 0, 0) },
                    { 6, 3, new TimeSpan(0, 0, 10, 0, 0) },
                    { 6, 4, new TimeSpan(0, 0, 5, 0, 0) },
                    { 6, 5, new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TopLevelRouteId",
                table: "Routes",
                column: "TopLevelRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_StopId",
                table: "Schedule",
                column: "StopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "TopLevelRoutes");
        }
    }
}
