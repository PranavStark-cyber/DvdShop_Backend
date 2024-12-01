using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DvdShop.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Directors_DirectorId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_DirectorId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RentalDate",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "DirectorId1",
                table: "Rentals",
                newName: "RentalDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RentalDays",
                table: "Rentals",
                newName: "DirectorId1");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalDate",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DirectorId1",
                table: "Rentals",
                column: "DirectorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Directors_DirectorId1",
                table: "Rentals",
                column: "DirectorId1",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
