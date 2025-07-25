using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABS.Hybrid.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationId);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.MaterialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    BatchSize = table.Column<int>(type: "int", nullable: false),
                    IsBatchSizeFixed = table.Column<bool>(type: "bit", nullable: false),
                    MixTime = table.Column<int>(type: "int", nullable: false),
                    MixTemperature = table.Column<int>(type: "int", nullable: false),
                    LowerTemperatureDeviation = table.Column<int>(type: "int", nullable: false),
                    UpperTemperatureDeviation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "StorageUnits",
                columns: table => new
                {
                    StorageUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageUnits", x => x.StorageUnitId);
                    table.ForeignKey(
                        name: "FK_StorageUnits_MaterialTypes_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "MaterialTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobNumber = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tonnage = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false),
                    StorageUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "MaterialTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_StorageUnits_StorageUnitId",
                        column: x => x.StorageUnitId,
                        principalTable: "StorageUnits",
                        principalColumn: "StorageUnitId");
                });

            migrationBuilder.CreateTable(
                name: "RecipeStorageUnits",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    StorageUnitId = table.Column<int>(type: "int", nullable: false),
                    Take = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeStorageUnits", x => new { x.RecipeId, x.StorageUnitId });
                    table.ForeignKey(
                        name: "FK_RecipeStorageUnits_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeStorageUnits_StorageUnits_StorageUnitId",
                        column: x => x.StorageUnitId,
                        principalTable: "StorageUnits",
                        principalColumn: "StorageUnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "DestinationId", "DestinationType", "Name" },
                values: new object[,]
                {
                    { 1, 1, "NK54 USG" },
                    { 2, 2, "Bin 1" },
                    { 3, 2, "Bin 2" },
                    { 4, 1, "NK71 YMH" }
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "MaterialTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Aggregate" },
                    { 2, "Bitumen" },
                    { 3, "Filler" },
                    { 4, "Fixed Additive" },
                    { 5, "Coldfeed" },
                    { 6, "Additive" },
                    { 7, "Reclaimed Asphalt" },
                    { 8, "Small Dose Additive" },
                    { 9, "Return Dust" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "BatchSize", "CreatedDate", "IsBatchSizeFixed", "IsValid", "LowerTemperatureDeviation", "MixTemperature", "MixTime", "Name", "Title", "UpperTemperatureDeviation", "VersionNumber" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2023, 6, 15, 10, 10, 56, 0, DateTimeKind.Unspecified), false, false, 0, 0, 0, "120A104", "AC32 HDM Base 40/60 REC (HYBRID)", 0, 0 },
                    { 2, 0, new DateTime(2024, 3, 21, 13, 15, 23, 0, DateTimeKind.Unspecified), false, false, 0, 0, 0, "120A104D", "AC32 HDM Base 40/60 DES (HYBRID)", 0, 0 },
                    { 3, 0, new DateTime(2024, 3, 21, 8, 15, 29, 0, DateTimeKind.Unspecified), false, false, 0, 0, 0, "120A104Z", "WARM AC32 HDM Base 40/60 REC (HYBRID)", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "MaterialId", "MaterialNumber", "MaterialTypeId", "Name", "StorageUnitId" },
                values: new object[,]
                {
                    { 1, 1, 1, "Dust/Sand", null },
                    { 2, 2, 1, "6mm", null },
                    { 3, 3, 1, "10mm", null },
                    { 4, 4, 1, "14mm", null },
                    { 5, 5, 1, "20mm", null },
                    { 6, 6, 1, "24mm+", null },
                    { 7, 50, 2, "40-60 Pen Bitumen", null },
                    { 8, 1001, 2, "Masterflex Binder", null }
                });

            migrationBuilder.InsertData(
                table: "StorageUnits",
                columns: new[] { "StorageUnitId", "MaterialTypeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Bin 1" },
                    { 2, 1, "Bin 2" },
                    { 3, 1, "Bin 3" },
                    { 4, 1, "Bin 4" },
                    { 5, 1, "Bin 5" },
                    { 6, 1, "Bin 6" },
                    { 7, 2, "Tank 1" },
                    { 8, 2, "Tank 2" },
                    { 9, 3, "Silo 1" },
                    { 10, 3, "Silo 2" },
                    { 11, 3, "Silo 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DestinationId",
                table: "Jobs",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RecipeId",
                table: "Jobs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_StorageUnitId",
                table: "Materials",
                column: "StorageUnitId",
                unique: true,
                filter: "[StorageUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Name",
                table: "Recipes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStorageUnits_StorageUnitId",
                table: "RecipeStorageUnits",
                column: "StorageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageUnits_MaterialTypeId",
                table: "StorageUnits",
                column: "MaterialTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "RecipeStorageUnits");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "StorageUnits");

            migrationBuilder.DropTable(
                name: "MaterialTypes");
        }
    }
}
