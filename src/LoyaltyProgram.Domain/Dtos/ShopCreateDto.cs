namespace LoyaltyProgram.Domain.Dtos
{
    public record ShopListCreateUpdateDto(
        string Name,
        string Address
    );

    public record ShopRankSystemListCreateDto(
        int? ShopId,
        string? Name,
        int PointsNeeded,
        RankStatus Rank,
        string? RankDescription
    );
}