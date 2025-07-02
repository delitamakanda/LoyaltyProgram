using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoyaltyProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class ClientIdRewardId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRewards_Clients_ClientId",
                table: "HistoryRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRewards_Rewards_RewardId",
                table: "HistoryRewards");

            migrationBuilder.AlterColumn<int>(
                name: "RewardId",
                table: "HistoryRewards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "HistoryRewards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRewards_Clients_ClientId",
                table: "HistoryRewards",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRewards_Rewards_RewardId",
                table: "HistoryRewards",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "RewardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRewards_Clients_ClientId",
                table: "HistoryRewards");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryRewards_Rewards_RewardId",
                table: "HistoryRewards");

            migrationBuilder.AlterColumn<int>(
                name: "RewardId",
                table: "HistoryRewards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "HistoryRewards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRewards_Clients_ClientId",
                table: "HistoryRewards",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryRewards_Rewards_RewardId",
                table: "HistoryRewards",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "RewardId");
        }
    }
}
