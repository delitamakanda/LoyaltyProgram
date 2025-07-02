namespace LoyaltyProgram.Domain.Dtos
{
    public record LoyaltyCardListDto(
        int LoyaltyCardId,
        string CardNumber,
        int Points,
        DateTime DateCreated,
        LoyaltyCardStatus Status,
        RankStatus Rank
    );
}