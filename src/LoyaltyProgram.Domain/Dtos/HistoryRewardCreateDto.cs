namespace LoyaltyProgram.Domain.Dtos
{
    public record HistoryRewardCreateDto(
        int RewardId,
        int ClientId,
        string? Description,
        DateTime CreatedDate
    );
}