﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DvdShop.Migrations
{
    /// <inheritdoc />
    public partial class init112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Rentals");
        }
    }
}
