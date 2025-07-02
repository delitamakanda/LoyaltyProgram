namespace LoyaltyProgram.Domain.Dtos
{
    public record LoyaltyCardUpdatetDto(
        string CardNumber,
        int Points,
        LoyaltyCardStatus Status,
        RankStatus Rank
    );
}