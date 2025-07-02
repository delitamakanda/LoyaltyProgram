namespace LoyaltyProgram.Domain.Dtos
{
    public record HistoryRewardListDto(
        int HistoryRewardId,
        DateTime CreatedDate,
        string Description,
        Reward Reward,
        Client Client
    );
}