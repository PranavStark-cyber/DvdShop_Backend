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
            migrationBuilder.AddColumn<Guid>(
                name: "UserRoleId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserRoleId",
                table: "Customers",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_UserRoles_UserRoleId",
                table: "Customers",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_UserRoles_UserRoleId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserRoleId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Customers");
        }
    }
}
