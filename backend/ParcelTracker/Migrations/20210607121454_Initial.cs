using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnAddress_Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    ReturnAddress_Street = table.Column<string>(type: "varchar(50)", nullable: true),
                    ReturnAddress_HouseNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    ReturnAddress_PostalCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    ReturnAddress_City = table.Column<string>(type: "varchar(50)", nullable: true),
                    ReturnAddress_Country = table.Column<string>(type: "varchar(150)", nullable: true),
                    DeliveryAddress_Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    DeliveryAddress_Street = table.Column<string>(type: "varchar(50)", nullable: true),
                    DeliveryAddress_HouseNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    DeliveryAddress_PostalCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    DeliveryAddress_City = table.Column<string>(type: "varchar(50)", nullable: true),
                    DeliveryAddress_Country = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParcelStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelStates_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcelStates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The parcel has been created.", "Created" },
                    { 2, "The parcel is currently in transit.", "InTransit" },
                    { 3, "The parcel is currently in being sorted.", "Sorting" },
                    { 4, "The parcel has been sorted and is awaiting delivery.", "Sorted" },
                    { 5, "The parcel is out for delivery.", "OutForDelivery" },
                    { 6, "The parcel has been delivered.", "Delivered" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParcelStates_ParcelId",
                table: "ParcelStates",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelStates_StateId",
                table: "ParcelStates",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name",
                table: "States",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelStates");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
