using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Rewards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "LoyaltyCards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HistoryRewards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyCards_ClientId",
                table: "LoyaltyCards",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyCards_Clients_ClientId",
                table: "LoyaltyCards",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyCards_Clients_ClientId",
                table: "LoyaltyCards");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyCards_ClientId",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "LoyaltyCards");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "HistoryRewards");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clients");
        }
    }
}
